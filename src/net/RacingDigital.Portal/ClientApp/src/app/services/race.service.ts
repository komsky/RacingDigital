import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Racecourse {
  id: number;
  name: string;
}

export interface Horse {
  id: number;
  name: string;
  description?: string | null;
  dateOfBirth?: string | null;
  colour?: string | null;
  identityUserId: string;
  owner: any;
}

export interface Jockey {
  id: number;
  name: string;
  country?: string | null;
}

export interface Note {
  id: number;
  content: string;
  createdAt: string;
  raceResultId: number;
  raceResult: any;
  identityUserId: number;
  identityUser: any;
}

export interface RaceResult {
  id: number;
  raceDate: string;
  raceName: string;
  racecourse: Racecourse;
  horse: Horse;
  jockey: Jockey;
  finishingPosition: number;
  notes: Note[];
}

@Injectable({
  providedIn: 'root'
})
export class RaceService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getMyRaces(): Observable<RaceResult[]> {
    return this.http.get<RaceResult[]>(`${this.baseUrl}api/races`);
  }
}
