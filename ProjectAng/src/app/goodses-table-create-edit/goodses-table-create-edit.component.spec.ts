import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoodsesTableCreateEditComponent } from './goodses-table-create-edit.component';

describe('GoodsesTableCreateEditComponent', () => {
  let component: GoodsesTableCreateEditComponent;
  let fixture: ComponentFixture<GoodsesTableCreateEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GoodsesTableCreateEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GoodsesTableCreateEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
