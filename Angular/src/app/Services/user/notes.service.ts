import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import {jwtDecode} from 'jwt-decode';


import { Observable } from 'rxjs';


export interface Note {

  id?: number;
 
  title: string;
 
  description: string;
 
  resources?: string;
 
  dateCreated?: Date;
 
  dateModified?: Date;
 
 }

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  private baseUrl = 'http://localhost:7123/api/NotesAPI'; // Gateway API URL

 constructor(private http: HttpClient) { }

 private getToken(): string | null {

  return localStorage.getItem('token');

 }

 private getUserIdFromToken(): string | null {

  const token = this.getToken();

  if (token) {

   const decoded: any = jwtDecode(token);

   return decoded.nameidentifier || decoded.sub || null;

  }

  return null;

 }

 private getAuthHeaders(): HttpHeaders {

  const token = this.getToken();

  return new HttpHeaders({

   'Authorization': `Bearer ${token}`

  });

 }

 getNotesByUser(): Observable<Note[]> {

  return this.http.get<Note[]>(`${this.baseUrl}`, { headers: this.getAuthHeaders() });

 }

 createNote(note: Note): Observable<Note> {

  return this.http.post<Note>(`${this.baseUrl}`, note, { headers: this.getAuthHeaders() });

 }

 updateNote(id: number, note: Note): Observable<any> {

  return this.http.put(`${this.baseUrl}/${id}`, note, { headers: this.getAuthHeaders() });

 }

 deleteNote(id: number): Observable<any> {

  return this.http.delete(`${this.baseUrl}/${id}`, { headers: this.getAuthHeaders() });

 }
}
