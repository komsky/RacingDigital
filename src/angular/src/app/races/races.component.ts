import { Component, OnInit } from '@angular/core';
import { RacesService, RaceResult } from './races.service';

@Component({
  selector: 'app-races',
  templateUrl: './races.component.html',
  styleUrls: ['./races.component.css']
})
export class RacesComponent implements OnInit {
  races: RaceResult[] = [];
  noteContent: { [key: number]: string } = {};

  constructor(private racesService: RacesService) { }

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.racesService.getRaces().subscribe(res => this.races = res);
  }

  bestJockey(horseId: number): string | undefined {
    const results = this.races.filter(r => r.horse.id === horseId);
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
      const race = this.races.find(r => r.id === raceId);
      if (race) {
        race.notes.push(note);
      }
      this.noteContent[raceId] = '';
    });
  }
}
