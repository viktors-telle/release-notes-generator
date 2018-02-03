import { Component, OnInit } from '@angular/core';
import { Project } from '../../common/classes/project';
import { Observable } from 'rxjs/Observable';
import { ProjectService } from '../../services/projects-service';

@Component({
    selector: 'projects',
    templateUrl: './projects.html',
    styleUrls: ['./projects.css'],
    providers: []
})
export class ProjectsComponent implements OnInit {
    projects: Observable<Project[]>;

    constructor(private projectService: ProjectService) {

    }

    ngOnInit() {
        this.projects = this.projectService.getProjects();        
    }

    private addProject() {
        
    }
}
