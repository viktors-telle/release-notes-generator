import {ReleaseNotesComponent} from '../release-notes/release-notes';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Repository } from '../../common/classes/Repository';
import { RepositoryService } from '../../services/repository-service';
import { RepositoryType } from '../../common/classes/RepositoryType';
import { ReleaseNotesService } from '../../services/release-notes-service';
import { ReleaseNote } from '../../common/classes/ReleaseNote';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ReleaseNotesGeneratorComponent } from '../release-notes/release-notes-generator';

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
                width: '90vh',
                data: { repositoryReleaseNotes: releaseNotes }
            });
    
            dialogRef.afterClosed().subscribe((result: any) => {                
            });
        });      
    }

    generateReleaseNotes(repository: Repository): void {
        let dialogRef = this.dialog.open(ReleaseNotesGeneratorComponent, {
            width: '90vh',
            data: { repository: repository }
        });

        dialogRef.afterClosed().subscribe((result: any) => {                
        });
    }

    getRepositoryName(id: number): string {
        return RepositoryType[id];
    }
}

class RepositoryReleaseNotes {
    repositoryId: number;
    releaseNotes: ReleaseNote[];
}
