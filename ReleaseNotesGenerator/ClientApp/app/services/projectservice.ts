import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class ProjectService {
    constructor(private httpClient: HttpClient) {
        
    }
}
