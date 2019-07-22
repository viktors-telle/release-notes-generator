import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import { ReleaseNotesService } from '../../services/release-notes-service';
import { MatDialogRef } from '@angular/material';
import { Repository } from '../../common/classes/Repository';
import { ProjectService } from '../../services/project-service';

@Component({
    selector: 'release-notes-generator',
    templateUrl: './release-notes-generator.html',
    styleUrls: ['./release-notes-generator.css'],
    providers: []
})
export class ReleaseNotesGeneratorComponent implements OnInit {
    public branchName: string;
    public fromDate: Date;
    public toDate: Date;
    public releaseNotesAreGenerating: boolean = false;
    public repository: Repository;

    constructor(
        private releaseNotesService: ReleaseNotesService,
        private projectService: ProjectService,
        public dialogRef: MatDialogRef<ReleaseNotesGeneratorComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) {
        this.repository = data.repository;
    }

    ngOnInit(): void {
    }

    generate(): void {
        this.releaseNotesAreGenerating = true;
        this.projectService.getProject(this.repository.projectId).subscribe((project) => {
            this.releaseNotesService.generateReleaseNotes(
                project.name, 
                this.repository.name, 
                this.branchName, 
                this.fromDate, 
                this.toDate)
                .subscribe(() => {
                        this.releaseNotesAreGenerating = false;
                        this.dialogRef.close();
                    },
                () => {
                        this.releaseNotesAreGenerating = false;
                    },
                () => {
                    this.releaseNotesAreGenerating = false;
                })
        });
    }

    close(): void {
        this.dialogRef.close();
    }
}
