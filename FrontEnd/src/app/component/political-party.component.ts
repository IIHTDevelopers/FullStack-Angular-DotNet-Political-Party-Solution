import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PoliticalPartyService } from '../service/political-party.service';
import { PoliticalParty } from '../model/political-party.model';

@Component({
    selector: 'app-political-party',
    templateUrl: './political-party.component.html',
    styleUrls: []
})
export class PoliticalPartyComponent implements OnInit {
    parties: PoliticalParty[] = [];
    partyForm: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private partyService: PoliticalPartyService
    ) {
        this.partyForm = this.formBuilder.group({
            name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
            founder: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(200)]],
            isDeleted: [false, Validators.required],
            politicalPartyId: [0]
        });
    }

    ngOnInit() {
        this.loadParties();
    }

    loadParties() {
        this.partyService.getAllPoliticalParties().subscribe(
            (parties: PoliticalParty[]) => {
                this.parties = parties;
            },
            (error) => {
                console.log('Error fetching parties:', error);
            }
        );
    }

    editParty(party: PoliticalParty) {
        this.partyForm.patchValue(party);
    }

    createParty() {
        if (this.partyForm.valid) {          
            const partyData = this.partyForm.value;
            console.log(partyData.name+partyData.politicalPartyId+partyData.founder+partyData.isDeleted);
        if (partyData.politicalPartyId!=0) {
            this.partyService.updatePoliticalParty(partyData).subscribe(
                (updatedParty: PoliticalParty) => {
                    const index = this.parties.findIndex(party => party.politicalPartyId === updatedParty.politicalPartyId);
                    if (index !== -1) {
                        this.parties[index] = updatedParty;
                    }
                    this.partyForm.reset();
                },
                (error) => {
                    console.log('Error updating party:', error);
                }
            );
        }else{
            this.partyService.createPoliticalParty(this.partyForm.value).subscribe(
                (party: PoliticalParty) => {
                    this.parties.push(party);
                    this.partyForm.reset();
                },
                (error) => {
                    console.log('Error creating party:', error);
                }
            );
        }}
    }

    deleteParty(id: number) {
        this.partyService.deletePoliticalParty(id).subscribe(
            () => {
                this.parties = this.parties.filter((party) => party.politicalPartyId !== id);
                console.log('this.parties.length', this.parties.length);
            },
            (error) => {
                console.log('Error deleting party:', error);
            }
        );
    }
    

    searchByName(event: any) {
        const name = event.target.value;
        this.partyService.searchPoliticalPartyByName(name).subscribe(
            (parties: PoliticalParty[]) => {
                this.parties = parties;
            },
            (error) => {
                console.log('Error searching parties:', error);
            }
        );
    }

    searchByFounder(event: any) {
        const founder = event.target.value;
        this.partyService.searchPoliticalPartyByFounder(founder).subscribe(
            (parties: PoliticalParty[]) => {
                this.parties = parties;
            },
            (error) => {
                console.log('Error searching parties:', error);
            }
        );
    }
}
