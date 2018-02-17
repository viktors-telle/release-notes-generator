import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Repository } from '../../common/classes/Repository';
import { RepositoryService } from '../../services/repository-service';
import { RepositoryType } from '../../common/classes/RepositoryType';

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

    getRepositoryName(id: number): string {
        return RepositoryType[id];
    }
}
