import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { RacesService, RaceResult } from './races.service';

@Component({
  selector: 'app-races',
  templateUrl: './races.component.html',
  styleUrls: ['./races.component.css']
})
export class RacesComponent implements OnInit, AfterViewInit {
  dataSource = new MatTableDataSource<RaceResult>();
  noteContent: { [key: number]: string } = {};
  bestJockeys: { [key: number]: string } = {};
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private racesService: RacesService) { }

  ngOnInit(): void {
    this.load();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  load(): void {
    this.racesService.getRaces().subscribe(res => this.dataSource.data = res);
  }

  fetchBestJockey(horseId: number): void {
    if (this.bestJockeys[horseId]) { return; }
    this.racesService.getBestJockey(horseId).subscribe({
      next: res => this.bestJockeys[horseId] = res.jockeyName,
      error: () => this.bestJockeys[horseId] = 'No data'
    });
  }

  bestJockey(horseId: number): string | undefined {
    const results = this.dataSource.data.filter(r => r.horse.id === horseId);
    const wins = new Map<string, number>();
    results.forEach(r => {
      if (r.finishingPosition === 1) {
        wins.set(r.jockey.name, (wins.get(r.jockey.name) || 0) + 1);
      }
    });
    let best: string | undefined;
    let max = 0;
    wins.forEach((count, jockey) => {
      if (count > max) {
        best = jockey;
        max = count;
      }
    });
    return best;
  }

  addNote(raceId: number): void {
    const content = this.noteContent[raceId];
    if (!content) { return; }
    this.racesService.addNote(raceId, content).subscribe(note => {
      const race = this.dataSource.data.find(r => r.id === raceId);
      if (race) {
        race.notes.push(note);
      }
      this.noteContent[raceId] = '';
    });
  }
}
