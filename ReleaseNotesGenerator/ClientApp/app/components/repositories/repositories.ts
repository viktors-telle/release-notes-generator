import {ReleaseNotesComponent} from '../release-notes/release-notes';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Repository } from '../../common/classes/Repository';
import { RepositoryService } from '../../services/repository-service';
import { RepositoryType } from '../../common/classes/RepositoryType';
import { ReleaseNote } from '../../common/classes/ReleaseNote';
import { MatDialog } from '@angular/material';
import { ReleaseNotesGeneratorComponent } from '../release-notes/release-notes-generator';
import { Common } from '../../common/common';

@Component({
    selector: 'repositories',
    templateUrl: './repositories.html',
    styleUrls: ['./repositories.css'],
    providers: []
})
export class RepositoriesComponent implements OnInit {
    repositories: Observable<Repository[]>;
    repositoryReleaseNotes: RepositoryReleaseNotes[] = [];

    constructor(private repositoryService: RepositoryService, public dialog: MatDialog) {
    }

    ngOnInit() {
        this.repositories = this.repositoryService.getRepositories();
    }

    openReleaseNotes(repository: Repository): void {
        this.repositoryService.getReleaseNotes(repository.id).subscribe((releaseNotes: ReleaseNote[]) => {
            let dialogRef = this.dialog.open(ReleaseNotesComponent, {
                width: '100vh',
                data: { repositoryReleaseNotes: releaseNotes }
            });
    
            dialogRef.afterClosed().subscribe(() => {
            });
        });      
    }

    generateReleaseNotes(repository: Repository): void {
        let dialogRef = this.dialog.open(ReleaseNotesGeneratorComponent, {
            width: '40vh',
            data: { repository: repository }
        });

        dialogRef.afterClosed().subscribe(() => {
        });
    }

    getRepositoryName(id: number): string {
        return Common.capitalizeFirstLetter(RepositoryType[id]);
    }
}

class RepositoryReleaseNotes {
    repositoryId: number;
    releaseNotes: ReleaseNote[];
}
