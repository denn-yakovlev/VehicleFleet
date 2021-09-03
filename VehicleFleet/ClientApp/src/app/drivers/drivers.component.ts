import {Component, Inject, TemplateRef} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap'

@Component({
  selector: 'app-drivers',
  templateUrl: './drivers.component.html'
})
export class DriversComponent {

  currentDriver : Driver;
  updateModal: NgbModalRef;
  createModal: NgbModalRef;
  drivers: Driver[];
  isUpdate : boolean;
  newDriver : Driver;

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    public modalService: NgbModal
  ) {
    http.get<Driver[]>(baseUrl + 'api/driver').subscribe(result => {
      this.drivers = result;
    }, error => console.error(error));
  }

  openDriverInfo(driverInfo: TemplateRef<any>, driver: Driver) {
    this.updateModal = this.modalService.open(driverInfo);
    this.currentDriver = driver;
  }

  closeDriverInfo() {
    this.updateModal.close();
    this.currentDriver = null;
  }

  saveChanges() {
    this.isUpdate = false;
    this.http.put(this.baseUrl + `api/driver/${this.currentDriver.id}`, this.currentDriver).subscribe(
      value => {
        console.log(value);
        this.closeDriverInfo();
      }, error=>console.log(error)
    );
  }

  delete() {
    this.http.delete(this.baseUrl + `api/driver/${this.currentDriver.id}`).subscribe(
      value => {
        delete this.drivers[this.drivers.indexOf(this.currentDriver)];
        this.closeDriverInfo();
      }, error=>console.log(error)
    );
  };

  openCreateModal(modal: TemplateRef<any>) {
    this.createModal = this.modalService.open(modal);
    this.newDriver = new Driver;
  }

  add() {
    this.http.post(this.baseUrl + 'api/driver', this.newDriver).subscribe(
      value => {
        this.drivers.push(this.newDriver)
        this.createModal.close();
        this.newDriver = null;
      }, error=>console.log(error)
    );

  }
}

export class Driver {
  id : number;
  fullName : string;
}
