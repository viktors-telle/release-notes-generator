import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { Repository } from "../common/classes/Repository";

@Injectable()
export class RepositoryService {
    baseUrl: string;

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getRepositories() : Observable<Repository[]> {
        return this.httpClient.get<Repository[]>(`${this.baseUrl}api/repositories`);
    }

    getRepository(id: string) : Observable<Repository> {
        return this.httpClient.get<Repository>(`${this.baseUrl}/api/repositories/${id}`);
    }

    insertRepository(repository: Repository) : Observable<Repository> {
        return this.httpClient.post<Repository>(`${this.baseUrl}/api/repositories`, repository);
    }

    updateRepository(repository: Repository) : Observable<Repository> {
        return this.httpClient.put<Repository>(`${this.baseUrl}/api/repositories/${repository.id}`, repository);
    }
}