import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_AkademikYil } from './models/ST_AkademikYil';
import { ST_AkademikYilService } from './services/ST_AkademikYil.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_AkademikYil',
	templateUrl: './sT_AkademikYil.component.html',
	styleUrls: ['./sT_AkademikYil.component.scss']
})
export class ST_AkademikYilComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_AkademikYilList:ST_AkademikYil[];
	sT_AkademikYil:ST_AkademikYil=new ST_AkademikYil();

	sT_AkademikYilAddForm: FormGroup;


	sT_AkademikYilId:number;

	constructor(private sT_AkademikYilService:ST_AkademikYilService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_AkademikYilList();
    }

	ngOnInit() {

		this.createST_AkademikYilAddForm();
	}


	getST_AkademikYilList() {
		this.sT_AkademikYilService.getST_AkademikYilList().subscribe(data => {
			this.sT_AkademikYilList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_AkademikYilAddForm.valid) {
			this.sT_AkademikYil = Object.assign({}, this.sT_AkademikYilAddForm.value)

			if (this.sT_AkademikYil.id == 0)
				this.addST_AkademikYil();
			else
				this.updateST_AkademikYil();
		}

	}

	addST_AkademikYil(){

		this.sT_AkademikYilService.addST_AkademikYil(this.sT_AkademikYil).subscribe(data => {
			this.getST_AkademikYilList();
			this.sT_AkademikYil = new ST_AkademikYil();
			jQuery('#st_akademikyil').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_AkademikYilAddForm);

		})

	}

	updateST_AkademikYil(){

		this.sT_AkademikYilService.updateST_AkademikYil(this.sT_AkademikYil).subscribe(data => {

			var index=this.sT_AkademikYilList.findIndex(x=>x.id==this.sT_AkademikYil.id);
			this.sT_AkademikYilList[index]=this.sT_AkademikYil;
			this.dataSource = new MatTableDataSource(this.sT_AkademikYilList);
            this.configDataTable();
			this.sT_AkademikYil = new ST_AkademikYil();
			jQuery('#st_akademikyil').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_AkademikYilAddForm);

		})

	}

	createST_AkademikYilAddForm() {
		this.sT_AkademikYilAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_AkademikYil(sT_AkademikYilId:number){
		this.sT_AkademikYilService.deleteST_AkademikYil(sT_AkademikYilId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_AkademikYilList=this.sT_AkademikYilList.filter(x=> x.id!=sT_AkademikYilId);
			this.dataSource = new MatTableDataSource(this.sT_AkademikYilList);
			this.configDataTable();
		})
	}

	getST_AkademikYilById(sT_AkademikYilId:number){
		this.clearFormGroup(this.sT_AkademikYilAddForm);
		this.sT_AkademikYilService.getST_AkademikYilById(sT_AkademikYilId).subscribe(data=>{
			this.sT_AkademikYil=data;
			this.sT_AkademikYilAddForm.patchValue(data);
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
