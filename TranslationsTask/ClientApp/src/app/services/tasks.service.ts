import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from './data.service';
import { Observable } from 'rxjs';
import { Task } from '../models/task';

@Injectable()
export class TasksService extends DataService {   
  constructor(private http: HttpClient) {
    super("/api/tasks");
  }

  getByProjectId(id: number): Observable<any> {
    return this.http.get(`${this.url}/byProjectId/${id}`);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${this.url}/${id}`);
  } 

  create(data: Task) {
    return this.http.post(this.url, data);
  }

  update(data: Task) {
    return this.http.put(this.url, data);
  }

  getTranslators(): Observable<any> {
    return this.http.get(`${this.url}/translatorsList`);
  }

  getProjects(): Observable<any> {
    return this.http.get(`${this.url}/projectsList`);
  }
}
