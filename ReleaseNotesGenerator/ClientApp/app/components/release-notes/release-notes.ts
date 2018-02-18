import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ReleaseNotesService } from '../../services/release-notes-service';
import { MatDialogRef } from '@angular/material';
import { ReleaseNote } from '../../common/classes/ReleaseNote';

@Component({
    selector: 'release-notes',
    templateUrl: './release-notes.html',
    styleUrls: ['./release-notes.css'],
    providers: []
})
export class ReleaseNotesComponent implements OnInit {
    public releaseNotes: ReleaseNote[] = [];

    constructor(
        private releaseNotesService: ReleaseNotesService,
        public dialogRef: MatDialogRef<ReleaseNotesComponent>,
        @Inject(MAT_DIALOG_DATA) public data:any) {
            this.releaseNotes = data.repositoryReleaseNotes;
    }

    ngOnInit(): void {
    }

    onNoClick(): void {
        this.dialogRef.close();
    }
}
