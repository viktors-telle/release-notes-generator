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
}