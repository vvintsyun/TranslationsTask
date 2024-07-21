import { Component, EventEmitter, OnInit } from '@angular/core';
import { TasksService } from '../services/tasks.service';
import { KeyValue } from '@angular/common';
import { Observable, forkJoin, switchMap, tap } from 'rxjs';
import { ReportsService } from '../services/reports.service';

@Component({
  selector: 'app-chart',
  styleUrls: ['./chart.component.sass'],
  templateUrl: './chart.component.html',
})
export class ChartComponent implements OnInit {

  constructor(
    private tasksService: TasksService,
    private reportsService: ReportsService,
  ) { }
  translators: KeyValue<number, string>[] = [];
  projects: KeyValue<number, string>[] = [];

  selectedProjects: number[] = [];
  selectedTranslators: number[] = [];

  ngOnInit(): void {
    this.getFilters().pipe(
      switchMap(_ => this.reportsService.getData({ projects: this.selectedProjects, translators: this.selectedTranslators }))       
    ).subscribe(res => {
      this.chartData = res.data;
      this.chartLabels = res.x;
    });
  }

  onFilterChanged(items: number[], filter: string) {
    switch (filter) {
      case 'projects': {
        this.selectedProjects = items;
        break;
      }
      case 'translators': {
        this.selectedTranslators = items;
        break;
      }
      default: throw new Error('Filter type is not defined');
    }

    this.reportsService.getData({ projects: this.selectedProjects, translators: this.selectedTranslators })
      .subscribe(res => {
        this.chartData = res.data;
        this.chartLabels = res.x;
      });
  }

  getFilters(): Observable<any> {
    const $translators = this.tasksService.getTranslators();
    const $projects = this.tasksService.getProjects();

    return forkJoin([$translators, $projects]).pipe(
      tap(([translators, projects]) => {
        this.projects = projects;
        this.translators = [{ key: -1, value: 'None' }, ...translators];

        this.selectedProjects = this.projects.map(x => x.key);
        this.selectedTranslators = this.translators.map(x => x.key);
      })
    );
  }

  chartData = [];

  chartOptions = {
    responsive: true
  };

  chartLabels = [];
}
