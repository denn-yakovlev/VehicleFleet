import {Component, Inject, TemplateRef, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap'
import {Vehicle} from "../vehicle";
import {Label, SingleDataSet} from 'ng2-charts'
import {NgForm} from "@angular/forms";
@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html'
})
export class VehiclesComponent {

  displayedVehicle : Vehicle;
  currentVehicle : Vehicle;
  updateModal: NgbModalRef;
  createModal: NgbModalRef;
  vehiclesList: Vehicle[];
  isUpdate : boolean;
  newVehicle : Vehicle;

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    public modalService: NgbModal
  ) {
    http.get<Vehicle[]>(baseUrl + 'api/vehicle').subscribe(result => {
      this.vehiclesList = result;
    }, error => console.error(error));
  }

  openVehicleInfo(vehicleInfo: TemplateRef<any>, vehicle: Vehicle) {
    this.updateModal = this.modalService.open(vehicleInfo);
    this.currentVehicle = vehicle;
    this.displayedVehicle = Object.assign({}, vehicle);
  }

  closeVehicleInfo() {
    this.updateModal.close();
    this.isUpdate = false;
    this.currentVehicle = null;
    this.displayedVehicle = null;
  }

  saveChanges(form: NgForm) {
    if (form.invalid)
      return;
    this.http.put(this.baseUrl + `api/vehicle/${this.displayedVehicle.id}`, this.displayedVehicle).subscribe(
      value => {
        console.log(value);
        this.closeVehicleInfo();
      }, error=>console.log(error)
    );
  }

  getChartData() : SingleDataSet
  {
    return this.displayedVehicle.bookCostByYears.map(cost => cost.cost);
  }

  getChartLabels() : Label
  {
    return this.displayedVehicle.bookCostByYears.map(cost => cost.year.toString());
  }

  delete() {
    this.http.delete(this.baseUrl + `api/vehicle/${this.displayedVehicle.id}`).subscribe(
      value => {
        delete this.vehiclesList[this.vehiclesList.indexOf(this.displayedVehicle)];
        this.closeVehicleInfo();
      }, error=>console.log(error)
    );
  };

  openCreateModal(modal: TemplateRef<any>) {
    this.createModal = this.modalService.open(modal);
    this.newVehicle = new Vehicle;
  }

  add() {
    this.http.post(this.baseUrl + 'api/vehicle', this.newVehicle).subscribe(
      value => {
        this.vehiclesList.push(this.newVehicle)
        this.createModal.close();
        this.newVehicle = null;
      }, error=>console.log(error)
    );

  }

  enableEditing() {
    this.isUpdate = true;
  }

  cancelChanges() {
    this.isUpdate = false;
    this.displayedVehicle = Object.assign({}, this.currentVehicle);
  }
}

