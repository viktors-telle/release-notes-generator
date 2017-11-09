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
    errorMessage: string;

    constructor(private projectService: ProjectService,
        private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder) {
    }

    ngOnInit() {
        let id = this.route.snapshot.params['id'];
        if (id !== '0') {
          this.getProject(id);         
          return;
        } 

        this.buildForm();
    }

    getProject(id: string) {
        this.projectService.getProject(id)
          .subscribe((project: Project) => {
            this.project = project;
            this.buildForm();
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

    submit({value, valid} : { value: Project, valid: boolean }) {
        if (!valid) {
            return;
        }

        if (value.id) {
            this.projectService.updateProject(value).subscribe((project: Project) => {
                this.router.navigate(["projects"]);
                console.log("Project updated!")
            },
            (err: any) => {
                console.log(err);
                this.errorMessage = "Error occured while updating project."
            });          
        } else {
            this.projectService.insertProject(value).subscribe((project: Project) => {
                this.router.navigate(["projects"]);
                console.log("Project inserted!")
            },
            (err: any) => {
                console.log(err);
                this.errorMessage = "Error occured while inserting new project."
            });            
        }
    }

    cancel(event: Event) {
        event.preventDefault();
        this.router.navigate(['/projects']);
      }
}
