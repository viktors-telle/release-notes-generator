import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ReleaseNotesService } from '../../services/release-notes-service';
import { MatDialogRef } from '@angular/material';
import { ReleaseNote } from '../../common/classes/ReleaseNote';
import { DatePipe } from '@angular/common';
import { Repository } from '../../common/classes/Repository';

@Component({
    selector: 'release-notes-generator',
    templateUrl: './release-notes-generator.html',
    styleUrls: ['./release-notes-generator.css'],
    providers: []
})
export class ReleaseNotesGeneratorComponent implements OnInit {
    private branchName: string;
    public repository: Repository;    

    constructor(
        private releaseNotesService: ReleaseNotesService,
        public dialogRef: MatDialogRef<ReleaseNotesGeneratorComponent>,
        @Inject(MAT_DIALOG_DATA) public data:any) {

    }

    ngOnInit(): void {
    }

    generate(): void {
        console.log("Release notes generated");
    }

    close(): void {
        this.dialogRef.close();
    }
}
