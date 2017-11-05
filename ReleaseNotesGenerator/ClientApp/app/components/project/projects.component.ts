import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../services/projectservice';
import { Project } from '../../common/interfaces/Project';

@Component({
    selector: 'projects',
    templateUrl: './projects.component.html',
    styleUrls: ['./projects.component.css'],
    providers: [ProjectService]
})
export class ProjectsComponent implements OnInit {
    projects: Project[];

    constructor(private projectService: ProjectService) {

    }

    ngOnInit() {
        this.projectService.getProjects().subscribe((projects: Project[]) => {
            console.log(projects);
            this.projects = projects;
        });
    }
}
