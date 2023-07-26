import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PoliticalPartyComponent } from './political-party.component';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { PoliticalPartyService } from '../service/political-party.service';
import { of } from 'rxjs';
import { PoliticalParty } from '../model/political-party.model';

describe('PoliticalPartyComponent', () => {
    let component: PoliticalPartyComponent;
    let fixture: ComponentFixture<PoliticalPartyComponent>;
    let mockPartyService: Partial<PoliticalPartyService>;
    let partyForm: FormGroup;

    beforeEach(() => {
        mockPartyService = {
            getAllPoliticalParties: jest.fn(() => of([
                { id: 1, name: 'Party 1', founder: 'Founder 1', isDeleted: false },
                { id: 2, name: 'Party 2', founder: 'Founder 2', isDeleted: false },
            ])),
            createPoliticalParty: jest.fn(() => of({ id: 3, name: 'New Party', founder: 'New Founder', isDeleted: false })),
            deletePoliticalParty: jest.fn(() => of()),
            searchPoliticalPartyByName: jest.fn(() => of([])),
            searchPoliticalPartyByFounder: jest.fn(() => of([])),
        };

        TestBed.configureTestingModule({
            imports: [ReactiveFormsModule],
            declarations: [PoliticalPartyComponent],
            providers: [
                { provide: PoliticalPartyService, useValue: mockPartyService },
            ],
        }).compileComponents();

        fixture = TestBed.createComponent(PoliticalPartyComponent);
        component = fixture.componentInstance;
        partyForm = component.partyForm;
        fixture.detectChanges();
    });

    it('should create the component', () => {
        expect(component).toBeTruthy();
    });

    it('should fetch all political parties on initialization', () => {
        expect(mockPartyService.getAllPoliticalParties).toHaveBeenCalled();
        expect(component.parties.length).toBe(2);
    });

    it('should display political parties in the template', () => {
        const partyList = fixture.nativeElement.querySelectorAll('li');
        expect(partyList.length).toBe(2);
        expect(partyList[0].textContent).toContain('Party 1 - Founder 1');
        expect(partyList[1].textContent).toContain('Party 2 - Founder 2');
    });

    it('should reset the form after creating a political party', () => {
        const newParty: PoliticalParty = { id: 3, name: 'New Party', founder: 'New Founder', isDeleted: false };
        partyForm.patchValue(newParty);
        component.createParty();
        expect(partyForm.value).toEqual({ name: null, founder: null, isDeleted: null });
    });

    it('should delete a political party and update the parties list', () => {
        const partyToDelete: PoliticalParty = {
            id: 1,
            name: 'Test Party',
            founder: 'Test Founder',
            isDeleted: false,
        };

        // Add the party to the component's parties list
        component.parties.push(partyToDelete);
        fixture.detectChanges();

        // Call the delete method in the component
        component.deleteParty(partyToDelete.id);
        fixture.detectChanges();

        // The party should be removed from the parties list
        expect(component.parties.length).toBe(0);
    });


    // it('should display a confirmation message after deleting a political party', () => {
    //     const partyToDelete = component.parties[0];
    //     component.deleteParty(partyToDelete.id);
    //     fixture.detectChanges();
    //     const messageElement = fixture.nativeElement.querySelector('.confirmation-message');
    //     expect(messageElement.textContent).toContain('Political party deleted successfully!');
    // });

    it('should not display the confirmation message initially', () => {
        const messageElement = fixture.nativeElement.querySelector('.confirmation-message');
        expect(messageElement).toBeFalsy();
    });

    it('should search political parties by name', () => {
        const searchName = 'Party 1';
        component.searchByName({ target: { value: searchName } });
        expect(mockPartyService.searchPoliticalPartyByName).toHaveBeenCalledWith(searchName);
        expect(component.parties.length).toBe(0);
    });

    it('should search political parties by founder', () => {
        const searchFounder = 'Founder 1';
        component.searchByFounder({ target: { value: searchFounder } });
        expect(mockPartyService.searchPoliticalPartyByFounder).toHaveBeenCalledWith(searchFounder);
        expect(component.parties.length).toBe(0);
    });

    it('should have required name and founder fields', () => {
        const nameControl = partyForm.get('name');
        const founderControl = partyForm.get('founder');

        expect(nameControl?.valid).toBeFalsy();
        expect(founderControl?.valid).toBeFalsy();

        const errors = nameControl?.errors;
        expect(errors?.['required']).toBeTruthy();

        const founderErrors = founderControl?.errors;
        expect(founderErrors?.['required']).toBeTruthy();
    });

    it('should have name field with minimum length validation', () => {
        const nameControl = partyForm.get('name');

        nameControl?.setValue('a');
        expect(nameControl?.valid).toBeFalsy();

        const errors = nameControl?.errors;
        expect(errors?.['minlength']).toBeTruthy();
    });

    it('should have name field with maximum length validation', () => {
        const nameControl = partyForm.get('name');

        nameControl?.setValue('a'.repeat(21));
        expect(nameControl?.valid).toBeFalsy();

        const errors = nameControl?.errors;
        expect(errors?.['maxlength']).toBeTruthy();
    });

    it('should have founder field with minimum length validation', () => {
        const founderControl = partyForm.get('founder');

        founderControl?.setValue('abcd');
        expect(founderControl?.valid).toBeFalsy();

        const errors = founderControl?.errors;
        expect(errors?.['minlength']).toBeTruthy();
    });

    it('should have founder field with maximum length validation', () => {
        const founderControl = partyForm.get('founder');

        founderControl?.setValue('a'.repeat(201));
        expect(founderControl?.valid).toBeFalsy();

        const errors = founderControl?.errors;
        expect(errors?.['maxlength']).toBeTruthy();
    });

    it('should be valid when all fields are filled correctly', () => {
        const nameControl = partyForm.get('name');
        const founderControl = partyForm.get('founder');

        nameControl?.setValue('Valid Party Name');
        founderControl?.setValue('Valid Founder Name');

        expect(partyForm.valid).toBeTruthy();
    });

    it('should be invalid when any field is not filled correctly', () => {
        const nameControl = partyForm.get('name');
        const founderControl = partyForm.get('founder');

        nameControl?.setValue('Inv'); // Name is too short
        founderControl?.setValue('Val');

        expect(partyForm.valid).toBeFalsy();
    });

    it('should display name field error message for invalid name', () => {
        const nameControl = component.partyForm.get('name');

        nameControl?.setValue('a');
        nameControl?.markAsTouched();
        fixture.detectChanges();

        // Check if the error message is present in the complete document
        const errorMessageText = 'Name must be at least 3 characters long.';
        const errorMessageInDocument = document.documentElement.textContent?.includes(errorMessageText);
        expect(errorMessageInDocument).toBe(true);
    });

    it('should display founder field error message for invalid founder', () => {
        const founderControl = partyForm.get('founder');

        founderControl?.setValue('abc');
        founderControl?.markAsTouched();
        fixture.detectChanges();

        const errorMessageText = 'Founder must be at least 5 characters long.';
        const errorMessageInDocument = document.documentElement.textContent?.includes(errorMessageText);
        expect(errorMessageInDocument).toBe(true);

        // const errorMessage = fixture.nativeElement.querySelector('.founder-error-message');
        // expect(errorMessage.textContent).toContain('Founder must be at least 5 characters long.');
    });

    it('should reset the error messages when valid input is provided', () => {
        const nameControl = partyForm.get('name');
        const founderControl = partyForm.get('founder');

        nameControl?.setValue('Valid Party Name');
        founderControl?.setValue('Valid Founder Name');

        // Trigger form validation
        nameControl?.markAsTouched();
        founderControl?.markAsTouched();
        fixture.detectChanges();

        const nameErrorMessage = fixture.nativeElement.querySelector('.name-error-message');
        const founderErrorMessage = fixture.nativeElement.querySelector('.founder-error-message');
        expect(nameErrorMessage).toBeFalsy();
        expect(founderErrorMessage).toBeFalsy();
    });

    // it('should display form level error message when form is invalid and submitted', () => {
    //     const nameControl = partyForm.get('name');
    //     const founderControl = partyForm.get('founder');

    //     nameControl?.setValue('Invalid Name');
    //     founderControl?.setValue('Valid Founder Name');

    //     // Trigger form validation
    //     nameControl?.markAsTouched();
    //     founderControl?.markAsTouched();
    //     fixture.detectChanges();

    //     // Submit the form
    //     component.createParty();
    //     fixture.detectChanges();

    //     const formErrorMessage = fixture.nativeElement.querySelector('.form-error-message');
    //     expect(formErrorMessage.textContent).toContain('Please fix the errors in the form.');
    // });

    it('should not display form level error message when form is valid and submitted', () => {
        const nameControl = partyForm.get('name');
        const founderControl = partyForm.get('founder');

        nameControl?.setValue('Valid Party Name');
        founderControl?.setValue('Valid Founder Name');

        // Trigger form validation
        nameControl?.markAsTouched();
        founderControl?.markAsTouched();
        fixture.detectChanges();

        // Submit the form
        component.createParty();
        fixture.detectChanges();

        const formErrorMessage = fixture.nativeElement.querySelector('.form-error-message');
        expect(formErrorMessage).toBeFalsy();
    });
});
