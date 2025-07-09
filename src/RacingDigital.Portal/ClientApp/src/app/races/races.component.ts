import { Component, OnInit } from '@angular/core';
import { RaceService, RaceResult } from '../services/race.service';

@Component({
  selector: 'app-races',
  templateUrl: './races.component.html'
})
export class RacesComponent implements OnInit {
  public races: RaceResult[] | null = null;

  constructor(private raceService: RaceService) {}

  ngOnInit(): void {
    this.raceService.getMyRaces().subscribe(r => this.races = r);
  }
}
