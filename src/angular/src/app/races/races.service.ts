import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface Note {
  id: number;
  content: string;
  createdAt: string;
}

export interface RaceResult {
  id: number;
  raceDate: string;
  raceName: string;
  racecourse: { id: number; name: string };
  horse: { id: number; name: string };
  jockey: { id: number; name: string };
  finishingPosition: number;
  notes: Note[];
}

@Injectable({ providedIn: 'root' })
export class RacesService {
  private apiUrl = environment.apiConfig.uri;

  constructor(private http: HttpClient) {}

  getRaces(): Observable<RaceResult[]> {
    return this.http.get<RaceResult[]>(this.apiUrl);
  }

  addNote(raceId: number, content: string): Observable<Note> {
    return this.http.post<Note>(`${this.apiUrl}/${raceId}/notes`, { content });
  }
}
