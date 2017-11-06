import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../services/projectservice';
import { Project } from '../../common/classes/project';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Repository } from '../../common/classes/Repository';

@Component({
    selector: 'project-edit',
    templateUrl: './project.edit.component.html',
    styleUrls: ['./project.edit.component.css'],
    providers: [ProjectService]
})
export class ProjectEditComponent implements OnInit {
    project: Project = {
        name: "",
        isDeactivated: false,
        apiKey: "",
        id: 0,
        repositories: [ ]
    };
    projectForm: FormGroup;

    constructor(private projectService: ProjectService,
        private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder) {
    }

    ngOnInit() {
        let id = this.route.snapshot.params['id'];
        if (id !== '0') {
          this.getProject(id);
        }   

        this.buildForm();
    }

    getProject(id: string) {
        this.projectService.getProject(id)
          .subscribe((project: Project) => {
            this.project = project;
          },
          (err: any) => console.log(err));
    }

    buildForm() {
        this.projectForm = this.formBuilder.group({
          name:  [this.project.name, Validators.required],
          apiKey:   [this.project.apiKey, Validators.required],
          isDeactivated:     [this.project.isDeactivated, Validators.required],      
        });
    }

    submit() {
        console.log("Form submitted");
    }

    cancel(event: Event) {
        event.preventDefault();
        this.router.navigate(['/projects']);
      }
}
