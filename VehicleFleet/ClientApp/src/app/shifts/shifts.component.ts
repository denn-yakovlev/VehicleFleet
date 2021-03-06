import {Component, Inject, TemplateRef} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap'
import {Shift} from "../shift";

@Component({
  selector: 'app-shifts',
  templateUrl: './shifts.component.html'
})
export class ShiftsComponent {

  displayedShift: Shift;
  currentShift: Shift;
  updateModal: NgbModalRef;
  createModal: NgbModalRef;
  shiftsList: Shift[];
  isUpdate: boolean;
  newShift: Shift;

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    public modalService: NgbModal
  ) {
    http.get<Shift[]>(baseUrl + 'api/shift').subscribe(result => {
      this.shiftsList = result;
    }, error => console.error(error));
  }

  openShiftInfo(shiftInfo: TemplateRef<any>, shift: Shift) {
    this.updateModal = this.modalService.open(shiftInfo);
    this.currentShift = shift;
    this.displayedShift = Object.assign({}, shift);
  }

  closeShiftInfo() {
    this.updateModal.close();
    this.displayedShift = null;
    this.currentShift = null;
  }

  saveChanges() {
    this.isUpdate = false;
    this.http.put(this.baseUrl + `api/shift/${this.displayedShift.id}`, this.displayedShift).subscribe(
      value => {
        console.log(value);
        this.closeShiftInfo();
      }, error => console.log(error)
    );
  }

  delete() {
    this.http.delete(this.baseUrl + `api/shift/${this.displayedShift.id}`).subscribe(
      value => {
        delete this.shiftsList[this.shiftsList.indexOf(this.displayedShift)];
        this.closeShiftInfo();
      }, error => console.log(error)
    );
  };

  openCreateModal(modal: TemplateRef<any>) {
    this.createModal = this.modalService.open(modal);
    this.newShift = new Shift;
  }

  add() {
    this.http.post(this.baseUrl + 'api/shift', this.newShift).subscribe(
      value => {
        this.shiftsList.push(this.newShift)
        this.createModal.close();
        this.newShift = null;
      }, error => console.log(error)
    );


  }

  enableEditing() {
    this.isUpdate = true;
  }

  cancelChanges() {
    this.isUpdate = false;
    this.displayedShift = Object.assign({}, this.currentShift);
  }
}
