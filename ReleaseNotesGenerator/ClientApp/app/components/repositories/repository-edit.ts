import { Component, OnInit } from '@angular/core';
import { Project } from '../../common/classes/project';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Repository } from '../../common/classes/Repository';
import { RepositoryType } from '../../common/classes/RepositoryType';
import { ProjectTrackingTool } from '../../common/classes/ProjectTrackingTool';
import { RepositoryService } from '../../services/repository-service';
import { ProjectService } from '../../services/project-service';

@Component({
    selector: 'repository-edit',
    templateUrl: './repository-edit.html',
    styleUrls: ['./repository-edit.css'],
    providers: []
})
export class RepositoryEditComponent implements OnInit {
    repository: Repository = {
        name: "",
        repositoryType: RepositoryType.git,
        url: "",
        owner: "",
        id: 0,
        project: <Project>{},
        projectId: 0,
        projectTrackingTool: <ProjectTrackingTool>{},
        projectTrackingToolId: 0,
        releaseNotes: [],
        showReleaseNotes: false
    };
    repositoryForm: FormGroup;
    errorMessage: string;
    formIsLoaded: boolean = false;
    repositoryTypes = RepositoryType;
    keys: string[];
    projects: Observable<Project[]>;

    constructor(private repositoryService: RepositoryService,
        private projectService: ProjectService,
        private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder) {
        this.keys = Object.keys(this.repositoryTypes).filter(Number);
    }

    ngOnInit() {
        let id = this.route.snapshot.params['id'];
        this.projects = this.projectService.getProjects();
        if (id !== '0') {
            this.getRepository(id);            
            return;
        }

        this.buildForm();
    }

    getRepository(id: string) {
        this.repositoryService.getRepository(id)
            .subscribe((repository: Repository) => {
                this.repository = repository;
                this.buildForm();
            },
            (err: any) => console.log(err));
    }

    buildForm() {
        this.repositoryForm = this.formBuilder.group({
            name: [this.repository.name, Validators.required],
            repositoryType: [this.repository.repositoryType, Validators.required],
            url: [this.repository.url, Validators.required],
            owner: [this.repository.owner, Validators.required],
            projectId: [this.repository.projectId, Validators.required],
        });
        this.formIsLoaded = true;
    }

    submit({ value, valid }: { value: Repository, valid: boolean }) {
        if (!valid) {
            return;
        }
        value.id = this.repository.id;
        if (value.id) {
            this.repositoryService.updateRepository(value).subscribe((repository: Repository) => {
                this.router.navigate(["repositories"]);
            },
                (err: any) => {
                    console.log(err);
                    this.errorMessage = "Error occured while updating repository."
                });
        } else {
            this.repositoryService.insertRepository(value).subscribe((repository: Repository) => {
                this.router.navigate(["repositories"]);
            },
                (err: any) => {
                    console.log(err);
                    this.errorMessage = "Error occured while inserting new repository."
                });
        }
    }

    cancel(event: Event) {
        event.preventDefault();
        this.router.navigate(['/repositories']);
    }
}