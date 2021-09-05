import {Component, Inject, TemplateRef} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap'
import {Driver} from "../driver";


@Component({
  selector: 'app-drivers',
  templateUrl: './drivers.component.html'
})
export class DriversComponent {

  displayedDriver : Driver;
  currentDriver : Driver;
  updateModal: NgbModalRef;
  createModal: NgbModalRef;
  driversList: Driver[];
  isUpdate : boolean;
  newDriver : Driver;

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    public modalService: NgbModal
  ) {
    http.get<Driver[]>(baseUrl + 'api/driver').subscribe(result => {
      this.driversList = result;
    }, error => console.error(error));
  }

  openDriverInfo(driverInfo: TemplateRef<any>, driver: Driver) {
    this.updateModal = this.modalService.open(driverInfo);
    this.currentDriver = driver;
    this.displayedDriver = Object.assign({}, driver);
  }

  closeDriverInfo() {
    this.updateModal.close();
    this.currentDriver = null;
    this.displayedDriver = null;
  }

  saveChanges() {
    this.isUpdate = false;
    this.http.put(this.baseUrl + `api/driver/${this.displayedDriver.id}`, this.displayedDriver).subscribe(
      value => {
        console.log(value);
        this.closeDriverInfo();
      }, error=>console.log(error)
    );
  }

  delete() {
    this.http.delete(this.baseUrl + `api/driver/${this.displayedDriver.id}`).subscribe(
      value => {
        delete this.driversList[this.driversList.indexOf(this.displayedDriver)];
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
        this.driversList.push(this.newDriver)
        this.createModal.close();
        this.newDriver = null;
      }, error=>console.log(error)
    );

  }

  enableEditing() {
    this.isUpdate = true;
  }

  cancelChanges() {
    this.isUpdate = false;
    this.displayedDriver = Object.assign({}, this.currentDriver);
  }
}

