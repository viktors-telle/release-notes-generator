import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ReleaseNotesService } from '../../services/release-notes-service';

@Component({
    selector: 'release-notes',
    templateUrl: './release-notes.html',
    styleUrls: ['./release-notes.css'],
    providers: []
})
export class ReleaseNotesComponent implements OnInit {     
    constructor(private releaseNotesService: ReleaseNotesService) {
    }

    ngOnInit(): void {
    }
}
