import {Component, Inject, TemplateRef} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap'
import {Driver} from "../driver";


@Component({
  selector: 'app-drivers',
  templateUrl: './drivers.component.html'
})
export class DriversComponent {

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
        delete this.driversList[this.driversList.indexOf(this.currentDriver)];
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
}

