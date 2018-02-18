import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ReleaseNotesService } from '../../services/release-notes-service';
import { MatDialogRef } from '@angular/material';
import { ReleaseNote } from '../../common/classes/ReleaseNote';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'release-notes',
    templateUrl: './release-notes.html',
    styleUrls: ['./release-notes.css'],
    providers: []
})
export class ReleaseNotesComponent implements OnInit {
    public releaseNotes: ReleaseNote[] = [];

    constructor(
        public dialogRef: MatDialogRef<ReleaseNotesComponent>,
        @Inject(MAT_DIALOG_DATA) public data:any) {
            this.releaseNotes = data.repositoryReleaseNotes;
    }

    ngOnInit(): void {
    }

    close(): void {
        this.dialogRef.close();
    }
}
