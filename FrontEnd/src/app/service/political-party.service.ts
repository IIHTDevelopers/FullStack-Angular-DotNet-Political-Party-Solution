import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PoliticalParty } from '../model/political-party.model';

@Injectable({
    providedIn: 'root'
})
export class PoliticalPartyService {
    private apiUrl = 'http://localhost:5000/parties';

    constructor(private http: HttpClient) { }

    getAllPoliticalParties(): Observable<PoliticalParty[]> {
        return this.http.get<PoliticalParty[]>(this.apiUrl);
        console.log(this.apiUrl);
    }

    createPoliticalParty(party: PoliticalParty): Observable<PoliticalParty> {
        console.log(party.name+party.politicalPartyId+party.founder+party.isDeleted);
        const temp = this.http.post<PoliticalParty>(this.apiUrl, party);
        alert(JSON.stringify(temp));
        return temp;
    }

    getPoliticalPartyById(id: number): Observable<PoliticalParty> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<PoliticalParty>(url);
        console.log(url);
    }

    updatePoliticalParty(party: PoliticalParty): Observable<PoliticalParty> {
        const url = `${this.apiUrl}/${party.politicalPartyId}`;
        return this.http.put<PoliticalParty>(url, party);
        console.log(url);
    }

    deletePoliticalParty(id: number): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<void>(url);
        console.log(url);
    }

    searchPoliticalPartyByName(name: string): Observable<PoliticalParty[]> {
        const url = `${this.apiUrl}/searchParty?name=${name}`;
        return this.http.get<PoliticalParty[]>(url);
        console.log(url);
    }

    searchPoliticalPartyByFounder(founder: string): Observable<PoliticalParty[]> {
        const url = `${this.apiUrl}/search?founderName=${founder}`;
        return this.http.get<PoliticalParty[]>(url);
        console.log(url);
    }
}
