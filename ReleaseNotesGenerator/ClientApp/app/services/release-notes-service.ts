import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { Project } from "../common/classes/project"
import { ReleaseNote } from "../common/classes/ReleaseNote";

@Injectable()
export class ReleaseNotesService {
    baseUrl: string;

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }   

    generateReleaseNotes(projectName: string, repositoryName: string, branchName: string, from: Date, until: Date): Observable<string> {
        return this
            .httpClient
            .get<string>(`${this.baseUrl}api/releasenotes/generate?projectName=${projectName}&repositoryName=${repositoryName}&branchName=${branchName}&from=${from.toISOString()}&until=${until.toISOString()}`);
    }
}