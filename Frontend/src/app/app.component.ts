import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs';
import { ApiService } from './service';
import { GenericResult } from './service.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  directionsItemDropdownDto = [
    { name: "Left" },
    { name: "Right" }
  ]
  isUpdating: boolean = false;

  selectedDirection: string = "Left";

  newRobotName: string = "";

  newTableInfo = {
    name: "",
    xSize: 0,
    ySize: 0
  }

  tablesItemDropdownDto: any[] = [];
  selectedTable: any;
  robotItemDropdownDto: any[] = [];
  selectedRobot: any;

  tableInfo = "";
  message: string = "Please select a table and a robot!";
  moveCount: number = 1;

  constructor(private apiService: ApiService) {
  }

  ngOnInit() {
    this.getTables();
    this.getRobots();
  }

  getRobots() {
    this.isUpdating = true;
    this.apiService.getAllRobot().pipe(finalize(() => (this.isUpdating = false)))
      .subscribe((result: GenericResult) => {
        this.robotItemDropdownDto = result.result;
      });
  }

  getTables() {
    this.isUpdating = true;
    this.apiService.getAllTable().pipe(finalize(() => (this.isUpdating = false))).subscribe((result: GenericResult) => {
      this.tablesItemDropdownDto = result.result;
    });
  }

  dropdownOnChange() {
    let robot = this.selectedRobot;
    let table = this.selectedTable;
    if (table != null && robot != null) {
      if (robot.tableId != table.id) {
        this.apiService.addRobotToTable(robot.id, table.id)
          .pipe(finalize(() => (this.isUpdating = false)))
          .subscribe((result: GenericResult) => {
            this.getRobots();
            this.selectedRobot = result.result;
            this.updateMessage(result.result);
          });
        return;
      }

      this.updateMessage(robot);
    }
  }

  addNewTable() {
    if (this.isUpdating) return;
    this.isUpdating = true;
    this.apiService.addTable(this.newTableInfo)
      .pipe(finalize(() => (this.isUpdating = false)))
      .subscribe((result: GenericResult) => {
        this.getTables();
      });
  }

  addNewRobot() {
    if(this.isUpdating) return;
    this.isUpdating = true;
    this.apiService.addRobot(this.newRobotName)
      .pipe(finalize(() => (this.isUpdating = false)))
      .subscribe((result: GenericResult) => {
        this.getRobots();
      });
  }

  rotateRobot() {
    if(this.isUpdating) return;
    this.isUpdating = true;
    this.apiService.rotateRobotByRotateDirection(this.selectedRobot.id, this.selectedDirection)
      .pipe(finalize(() => (this.isUpdating = false)))
      .subscribe((result: GenericResult) => {
        this.getRobots();
        this.selectedRobot = result.result;
        this.updateMessage(result.result);
      })
  }

  moveRobot() {
    if(this.isUpdating) return;
    this.isUpdating = true;
    this.apiService.moveRobotById(this.selectedRobot.id, this.moveCount)
      .pipe(finalize(() => (this.isUpdating = false)))
      .subscribe((result: GenericResult) => {
        this.getRobots();
        this.selectedRobot = result.result;
        this.updateMessage(result.result);
      })
  }

  updateMessage(robot: any) {
    this.tableInfo = "Table: " + this.selectedTable.name + " has the size of X: " + this.selectedTable.xSize + " and Y: " + this.selectedTable.ySize;
    this.message = "Robot: " + robot.name + " is at position X: " + robot.currentPosition.x + " Y: " + robot.currentPosition.y + " Facing: " + robot.direction;
  }

  deleteTable() {
    this.isUpdating = true;
    this.apiService.deleteTableById(this.selectedTable.id)
      .pipe(finalize(() => (this.isUpdating = false)))
      .subscribe(() => {
        this.getTables();
        this.selectedTable = null;
      })
  }

  deleteRobot() {
    this.isUpdating = true;
    this.apiService.deleteRobotById(this.selectedRobot.id)
      .pipe(finalize(() => (this.isUpdating = false)))
      .subscribe(() => {
        this.getRobots();
        this.selectedRobot = null;
      })
  }

  manuallyRefreshDropdown() {
    this.getTables();
    this.getRobots();
  }
}
