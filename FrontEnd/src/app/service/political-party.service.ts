import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PoliticalParty } from '../model/political-party.model';

@Injectable({
    providedIn: 'root'
})
export class PoliticalPartyService {
    private apiUrl = 'http://localhost:8080/api/parties';

    constructor(private http: HttpClient) { }

    getAllPoliticalParties(): Observable<PoliticalParty[]> {
        return this.http.get<PoliticalParty[]>(this.apiUrl);
    }

    createPoliticalParty(party: PoliticalParty): Observable<PoliticalParty> {
        return this.http.post<PoliticalParty>(this.apiUrl, party);
    }

    getPoliticalPartyById(id: number): Observable<PoliticalParty> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<PoliticalParty>(url);
    }

    updatePoliticalParty(party: PoliticalParty): Observable<PoliticalParty> {
        const url = `${this.apiUrl}/${party.id}`;
        return this.http.put<PoliticalParty>(url, party);
    }

    deletePoliticalParty(id: number): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
    }

    searchPoliticalPartyByName(name: string): Observable<PoliticalParty[]> {
        const url = `${this.apiUrl}/search?name=${name}`;
        return this.http.get<PoliticalParty[]>(url);
    }

    searchPoliticalPartyByFounder(founder: string): Observable<PoliticalParty[]> {
        const url = `${this.apiUrl}/search?founder=${founder}`;
        return this.http.get<PoliticalParty[]>(url);
    }
}
