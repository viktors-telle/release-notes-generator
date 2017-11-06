import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { Project } from "../common/classes/project"

@Injectable()
export class ProjectService {
    baseUrl: string;

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getProjects() : Observable<Project[]> {
        return this.httpClient.get<Project[]>(`${this.baseUrl}/api/projects`);
    }
}
