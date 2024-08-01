import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_OgrenciDurum } from './models/ST_OgrenciDurum';
import { ST_OgrenciDurumService } from './services/ST_OgrenciDurum.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_OgrenciDurum',
	templateUrl: './sT_OgrenciDurum.component.html',
	styleUrls: ['./sT_OgrenciDurum.component.scss']
})
export class ST_OgrenciDurumComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_OgrenciDurumList:ST_OgrenciDurum[];
	sT_OgrenciDurum:ST_OgrenciDurum=new ST_OgrenciDurum();

	sT_OgrenciDurumAddForm: FormGroup;


	sT_OgrenciDurumId:number;

	constructor(private sT_OgrenciDurumService:ST_OgrenciDurumService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_OgrenciDurumList();
    }

	ngOnInit() {

		this.createST_OgrenciDurumAddForm();
	}


	getST_OgrenciDurumList() {
		this.sT_OgrenciDurumService.getST_OgrenciDurumList().subscribe(data => {
			this.sT_OgrenciDurumList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_OgrenciDurumAddForm.valid) {
			this.sT_OgrenciDurum = Object.assign({}, this.sT_OgrenciDurumAddForm.value)

			if (this.sT_OgrenciDurum.id == 0)
				this.addST_OgrenciDurum();
			else
				this.updateST_OgrenciDurum();
		}

	}

	addST_OgrenciDurum(){

		this.sT_OgrenciDurumService.addST_OgrenciDurum(this.sT_OgrenciDurum).subscribe(data => {
			this.getST_OgrenciDurumList();
			this.sT_OgrenciDurum = new ST_OgrenciDurum();
			jQuery('#st_ogrencidurum').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_OgrenciDurumAddForm);

		})

	}

	updateST_OgrenciDurum(){

		this.sT_OgrenciDurumService.updateST_OgrenciDurum(this.sT_OgrenciDurum).subscribe(data => {

			var index=this.sT_OgrenciDurumList.findIndex(x=>x.id==this.sT_OgrenciDurum.id);
			this.sT_OgrenciDurumList[index]=this.sT_OgrenciDurum;
			this.dataSource = new MatTableDataSource(this.sT_OgrenciDurumList);
            this.configDataTable();
			this.sT_OgrenciDurum = new ST_OgrenciDurum();
			jQuery('#st_ogrencidurum').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_OgrenciDurumAddForm);

		})

	}

	createST_OgrenciDurumAddForm() {
		this.sT_OgrenciDurumAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_OgrenciDurum(sT_OgrenciDurumId:number){
		this.sT_OgrenciDurumService.deleteST_OgrenciDurum(sT_OgrenciDurumId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_OgrenciDurumList=this.sT_OgrenciDurumList.filter(x=> x.id!=sT_OgrenciDurumId);
			this.dataSource = new MatTableDataSource(this.sT_OgrenciDurumList);
			this.configDataTable();
		})
	}

	getST_OgrenciDurumById(sT_OgrenciDurumId:number){
		this.clearFormGroup(this.sT_OgrenciDurumAddForm);
		this.sT_OgrenciDurumService.getST_OgrenciDurumById(sT_OgrenciDurumId).subscribe(data=>{
			this.sT_OgrenciDurum=data;
			this.sT_OgrenciDurumAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
