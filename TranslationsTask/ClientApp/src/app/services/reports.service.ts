import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from './data.service';
import { Observable } from 'rxjs';

export interface ReportFilters {
  projects: number[];
  translators: number[];
}

@Injectable()
export class ReportsService extends DataService {  
  constructor(private http: HttpClient) {
    super("/api/reports");
  }

  getData(filters: ReportFilters): Observable<any> {
    const params = {
      projects: filters.projects,
      translators: filters.translators
    };
    return this.http.get(this.url, { params: params });
  }
}
