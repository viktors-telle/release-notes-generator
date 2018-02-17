import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Repository } from '../../common/classes/Repository';
import { RepositoryService } from '../../services/repository-service';
import { RepositoryType } from '../../common/classes/RepositoryType';
import { ReleaseNotesService } from '../../services/release-notes-service';
import { ReleaseNote } from '../../common/classes/ReleaseNote';

@Component({
    selector: 'repositories',
    templateUrl: './repositories.html',
    styleUrls: ['./repositories.css'],
    providers: []
})
export class RepositoriesComponent implements OnInit {
    repositories: Observable<Repository[]>;
    showReleaseNotes: boolean = false;

    repositoryReleaseNotes: RepositoryReleaseNotes[] = [];

    constructor(private repositoryService: RepositoryService) {
    }

    ngOnInit() {
        this.repositories = this.repositoryService.getRepositories();
    }

    getRepositoryName(id: number): string {
        return RepositoryType[id];
    }

    getReleaseNotes(repository: Repository) {
        if (repository.showReleaseNotes === true) {
            repository.showReleaseNotes = false;
            return;
        }
        let releaseNotes = this.repositoryService.getReleaseNotes(repository.id).subscribe((releaseNotes) => {
            for (var i = this.repositoryReleaseNotes.length - 1; i >= 0; --i) {
                if (this.repositoryReleaseNotes[i].repositoryId === repository.id) {
                    this.repositoryReleaseNotes.splice(i, 1);                    
                }
            }
            let repositoryReleaseNotes = new RepositoryReleaseNotes();
            repositoryReleaseNotes.releaseNotes = releaseNotes,
                repositoryReleaseNotes.repositoryId = repository.id;
            this.repositoryReleaseNotes.push(repositoryReleaseNotes);
            repository.showReleaseNotes = true;
        });
    }
}

class RepositoryReleaseNotes {
    repositoryId: number;
    releaseNotes: ReleaseNote[];
}
