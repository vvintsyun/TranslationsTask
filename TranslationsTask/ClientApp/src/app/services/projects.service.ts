import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from './data.service';
import { Observable } from 'rxjs';
import { Project } from '../models/project';

@Injectable()
export class ProjectsService extends DataService {  
  constructor(private http: HttpClient) {
    super("/api/projects");
  }

  getData(): Observable<any> {
    return this.http.get(this.url);
  }

  create(data: Project) {
    return this.http.post(this.url, data);
  }
}
