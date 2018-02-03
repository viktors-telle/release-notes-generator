import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Repository } from '../../common/classes/Repository';
import { RepositoryService } from '../../services/repository-service';

@Component({
    selector: 'repositories',
    templateUrl: './repositories.html',
    styleUrls: ['./repositories.css'],
    providers: []
})
export class RepositoriesComponent implements OnInit {
    repositories: Observable<Repository[]>;

    constructor(private repositoryService: RepositoryService) {

    }

    ngOnInit() {
        this.repositories = this.repositoryService.getRepositories();        
    }

    private addProject() {
        
    }
}
