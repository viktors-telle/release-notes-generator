import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../services/projectservice';
import { Project } from '../../common/classes/project';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.css'],
    providers: [ProjectService]
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
