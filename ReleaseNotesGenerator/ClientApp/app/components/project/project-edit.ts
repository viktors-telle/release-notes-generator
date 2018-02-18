import { Component, OnInit } from '@angular/core';
import { Project } from '../../common/classes/project';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Repository } from '../../common/classes/Repository';
import { ProjectService } from '../../services/project-service';

@Component({
    selector: 'project-edit',
    templateUrl: './project-edit.html',
    styleUrls: ['./project-edit.css'],
    providers: []
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
    formIsLoaded: boolean = false;

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
        this.projectService.getProject(parseInt(id))
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
        this.formIsLoaded = true;
    }

    submit({value, valid} : { value: Project, valid: boolean }) {
        if (!valid) {
            return;
        }
        value.id = this.project.id;
        if (value.id) {
            this.projectService.updateProject(value).subscribe((project: Project) => {
                this.router.navigate(["projects"]);
            },
            (err: any) => {
                console.log(err);
                this.errorMessage = "Error occured while updating project."
            });          
        } else {
            this.projectService.insertProject(value).subscribe((project: Project) => {
                this.router.navigate(["projects"]);
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
