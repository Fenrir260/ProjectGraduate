import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoodsesTableComponent } from './goodses-table.component';

describe('GoodsesTableComponent', () => {
  let component: GoodsesTableComponent;
  let fixture: ComponentFixture<GoodsesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GoodsesTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GoodsesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
