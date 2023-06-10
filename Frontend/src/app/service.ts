import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, share, take } from 'rxjs';
import { GenericResult } from './service.model';
import { DialogService } from 'primeng/dynamicdialog';
import { ModalComponent } from './modal/modal.component';

@Injectable({
    providedIn: 'root',
    deps: [HttpClient, DialogService],
})
export class ApiService {
    private apiEndpoint = 'https://localhost:7249/api/v1.0/services/MainPage';
    constructor(private http: HttpClient, private dialogService: DialogService) { }

    // Get all tables
    getAllTable(): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/GetAllTable`;
        let observable = this.http.get<GenericResult>(url);

        //Example of using the validate method
        return this.validate(observable);
    }

    // Get all robots
    getAllRobot(): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/GetAllRobot`;
        return this.validate(this.http.get<GenericResult>(url));
    }

    // Move robot by ID
    moveRobotById(id: number, moveCount: number): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/MoveRobot/${id}`;
        return this.validate(this.http.post<GenericResult>(url, moveCount));
    }

    // Rotate robot by rotate direction
    rotateRobotByRotateDirection(id: number, direction: string): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/RotateRobotByRotateDirection/${id}`;
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        let body = JSON.stringify(direction);
        return this.validate(this.http.post<GenericResult>(url, body, httpOptions));
    }

    // Add robot to table
    addRobotToTable(robotId: number, tableId: number): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/AddRobotToTable/${robotId}/${tableId}`;
        return this.validate(this.http.post<GenericResult>(url, null));
    }

    // Add robot
    addRobot(name: string): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/AddRobot`;
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        let body = JSON.stringify(name);
        return this.validate(this.http.post<GenericResult>(url, body, httpOptions));
    }

    // Add table
    addTable(param: { name: string, xSize: number, ySize: number }): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/AddTable`;
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        let body = JSON.stringify(param);
        return this.validate(this.http.post<GenericResult>(url, body, httpOptions));
    }

    // Delete table by ID
    deleteTableById(id: number): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/DeleteTable/${id}`;
        return this.validate(this.http.delete<GenericResult>(url));
    }

    // Delete robot by ID
    deleteRobotById(id: number): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/DeleteRobot/${id}`;
        return this.validate(this.http.delete<GenericResult>(url));
    }

    // Update robot by ID
    updateRobotById(id: number, name: string): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/UpdateRobot/${id}`;
        return this.validate(this.http.put<GenericResult>(url, name));
    }

    // Update table by ID
    updateTableById(id: number, body: { name: string, xSize: number, ySize: number }): Observable<GenericResult> {
        const url = `${this.apiEndpoint}/UpdateTable/${id}`;
        return this.validate(this.http.put<GenericResult>(url, body));
    }


    /**
     * This method is used to validate the form
     * as an example of how to deal with every call to the API
     */
    private validate(observable: Observable<any>): Observable<any> {
        let openModal = this.openModal;
        let dialogService = this.dialogService;
        let isModalOpen = this.isModalOpen;
        let toReturn = observable.pipe(share());
        toReturn.pipe(take(1))
            .subscribe({
                error(x: { error: GenericResult }) {
                    //If expectedError.error is not null we rest assure that the error is something we handled or expected
                    let expectedError = x.error;
                    if (expectedError != null) {
                        //Show this error to client maybe open modal or something
                        openModal(expectedError.error?.details ?? '', dialogService, isModalOpen);
                        //We could also show the details of the error
                        return;
                    }

                    //Errors we don't know about may not be good to show to the client
                    /**
                     * let unexpectedError = x as any; 
                     * console.error('Something wrong occurred! ' + unexpectedError?.error?.message ?? '');
                     */
                }
            });

        return toReturn;
    }

    private isModalOpen = false;

    private openModal(errorMessage: string, dialogService: DialogService, isModalOpen: boolean) {
        if (!isModalOpen) {
            const ref = dialogService.open(ModalComponent, {
                header: '',
                width: '70%',
                contentStyle: {
                    'max-height': '500px',
                    'overflow': 'auto'
                },
                baseZIndex: 10000,
                data: { message: errorMessage }
            });

            ref.onClose.subscribe(() => {
                isModalOpen = false;
            });

            isModalOpen = true;
        }
    }
}