import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_DersGunu } from './models/ST_DersGunu';
import { ST_DersGunuService } from './services/ST_DersGunu.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_DersGunu',
	templateUrl: './sT_DersGunu.component.html',
	styleUrls: ['./sT_DersGunu.component.scss']
})
export class ST_DersGunuComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_DersGunuList:ST_DersGunu[];
	sT_DersGunu:ST_DersGunu=new ST_DersGunu();

	sT_DersGunuAddForm: FormGroup;


	sT_DersGunuId:number;

	constructor(private sT_DersGunuService:ST_DersGunuService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_DersGunuList();
    }

	ngOnInit() {

		this.createST_DersGunuAddForm();
	}


	getST_DersGunuList() {
		this.sT_DersGunuService.getST_DersGunuList().subscribe(data => {
			this.sT_DersGunuList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_DersGunuAddForm.valid) {
			this.sT_DersGunu = Object.assign({}, this.sT_DersGunuAddForm.value)

			if (this.sT_DersGunu.id == 0)
				this.addST_DersGunu();
			else
				this.updateST_DersGunu();
		}

	}

	addST_DersGunu(){

		this.sT_DersGunuService.addST_DersGunu(this.sT_DersGunu).subscribe(data => {
			this.getST_DersGunuList();
			this.sT_DersGunu = new ST_DersGunu();
			jQuery('#st_dersgunu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersGunuAddForm);

		})

	}

	updateST_DersGunu(){

		this.sT_DersGunuService.updateST_DersGunu(this.sT_DersGunu).subscribe(data => {

			var index=this.sT_DersGunuList.findIndex(x=>x.id==this.sT_DersGunu.id);
			this.sT_DersGunuList[index]=this.sT_DersGunu;
			this.dataSource = new MatTableDataSource(this.sT_DersGunuList);
            this.configDataTable();
			this.sT_DersGunu = new ST_DersGunu();
			jQuery('#st_dersgunu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersGunuAddForm);

		})

	}

	createST_DersGunuAddForm() {
		this.sT_DersGunuAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_DersGunu(sT_DersGunuId:number){
		this.sT_DersGunuService.deleteST_DersGunu(sT_DersGunuId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_DersGunuList=this.sT_DersGunuList.filter(x=> x.id!=sT_DersGunuId);
			this.dataSource = new MatTableDataSource(this.sT_DersGunuList);
			this.configDataTable();
		})
	}

	getST_DersGunuById(sT_DersGunuId:number){
		this.clearFormGroup(this.sT_DersGunuAddForm);
		this.sT_DersGunuService.getST_DersGunuById(sT_DersGunuId).subscribe(data=>{
			this.sT_DersGunu=data;
			this.sT_DersGunuAddForm.patchValue(data);
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
