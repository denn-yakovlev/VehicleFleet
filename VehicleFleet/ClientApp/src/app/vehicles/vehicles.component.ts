import {Component, Inject, TemplateRef, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap'
import {Vehicle} from "../vehicle";
import {SingleDataSet} from 'ng2-charts'
@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html'
})
export class VehiclesComponent {

  currentVehicle : Vehicle;
  updateModal: NgbModalRef;
  createModal: NgbModalRef;
  vehiclesList: Vehicle[];
  isUpdate : boolean;
  newVehicle : Vehicle;

  // @ViewChild(HTMLCanvasElement, {static:false}) chartCanvas: HTMLCanvasElement;

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
  }

  closeVehicleInfo() {
    this.updateModal.close();
    this.currentVehicle = null;
  }

  saveChanges() {
    this.isUpdate = false;
    this.http.put(this.baseUrl + `api/vehicle/${this.currentVehicle.id}`, this.currentVehicle).subscribe(
      value => {
        console.log(value);
        this.closeVehicleInfo();
      }, error=>console.log(error)
    );
  }

  convertBookCostByYears() : SingleDataSet
  {
    return this.currentVehicle.bookCostByYears.map((cost) => ({x: cost.year, y: cost.cost}));
  }

  delete() {
    this.http.delete(this.baseUrl + `api/vehicle/${this.currentVehicle.id}`).subscribe(
      value => {
        delete this.vehiclesList[this.vehiclesList.indexOf(this.currentVehicle)];
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
}

