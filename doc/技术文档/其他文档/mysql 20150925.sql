
CREATE TABLE `brand` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `Code` varchar(50) DEFAULT NULL COMMENT 'Ʒ�ƴ���',
  `Name` varchar(50) DEFAULT NULL COMMENT 'Ʒ������ Ψһ',
  `ParentID` int(10) DEFAULT NULL COMMENT '����id',
  `Seq` int(4) DEFAULT NULL COMMENT '���� Ĭ��0',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_brand_Code` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��ƷƷ�Ʊ� '
;

CREATE TABLE `category` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `Code` varchar(50) DEFAULT NULL COMMENT '�������',
  `Name` varchar(50) DEFAULT NULL COMMENT '�������� Ψһ',
  `ParentID` int(10) DEFAULT NULL COMMENT '����id',
  `Seq` int(4) DEFAULT '0' COMMENT '���� Ĭ��0',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��Ʒ�����   '
;

CREATE TABLE `classs` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ClassName` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8
;
INSERT classs (ClassName) VALUES ( '1��');
INSERT classs (ClassName) VALUES ( '2��');
INSERT classs (ClassName) VALUES ( '3��');
INSERT classs (ClassName) VALUES ( '4��');
INSERT classs (ClassName) VALUES ( '4��');
INSERT classs (ClassName) VALUES ( '5��');

CREATE TABLE `logistics` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL COMMENT '��������',
  `WebUrl` varchar(100) DEFAULT NULL COMMENT '��˾��ַ',
  `IsSetArea` int(4) DEFAULT NULL COMMENT '�Ƿ��������� 1 �� 0 ��   ��Ϊ���ʱ��Ĭ��Ϊ��������',
  `Tags` varchar(150) DEFAULT NULL COMMENT '��ǩ',
  `KeyWords` varchar(200) DEFAULT NULL COMMENT '�ؼ���',
  `IsEnable` int(4) DEFAULT NULL COMMENT '�Ƿ���� 1��0 ��',
  `Code` varchar(50) DEFAULT NULL COMMENT '��������',
  `Seq` int(4) DEFAULT NULL COMMENT '���� Ĭ��0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='������Ϣ��'
;

CREATE TABLE `logisticsAreaMap` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `LogisticsID` int(10) DEFAULT NULL COMMENT '������˾ID',
  `AreaID` int(10) DEFAULT NULL COMMENT '����id(��һ����id)',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��������������Ϣ��'
;

CREATE TABLE `ord_base` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OutOrderCode` varchar(1000) DEFAULT NULL COMMENT '�ⲿ�����š�ϵͳ����ʱ�Ķ����ţ�ERP�ڲ��ֶ����ʱ���û��������Ϊ��',
  `ErpOrderCode` varchar(100) DEFAULT NULL COMMENT 'ϵͳ�����š�ERP�Զ����ɵĶ����ţ������ظ�������Ϊ�ա�����Ψһ��ʶ',
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ���',
  `CreateType` int(1) DEFAULT NULL COMMENT '�������� 0��API���أ�1���ֶ���2������',
  `OrderSource` int(4) DEFAULT NULL COMMENT '������Դ ö��',
  `ShopId` int(10) DEFAULT NULL COMMENT '����ID',
  `OrderType` int(1) DEFAULT NULL COMMENT '�������� 0���Է���1������',
  `BuyNickName` varchar(50) DEFAULT NULL COMMENT '����ǳ�',
  `BuyName` varchar(50) DEFAULT NULL COMMENT '�������',
  `BuyTel` varchar(50) DEFAULT NULL COMMENT '��ҵ绰',
  `BuyAddr` varchar(100) DEFAULT NULL COMMENT '��ҵ�ַ',
  `BuyPostCode` varchar(50) DEFAULT NULL COMMENT '����ʱ�',
  `BuyMessage` varchar(4000) DEFAULT NULL COMMENT '�������',
  `SellerRemark` varchar(4000) DEFAULT NULL COMMENT '���ұ�ע',
  `OrderStatus` varchar(50) DEFAULT NULL COMMENT '����״̬�������ɡ��ȴ����䡢�ȴ��������������С��������ַ�������������������ȡ���������ɹ����������� �������ö������',
  `OrderAmount` decimal(18,3) DEFAULT NULL COMMENT '�������',
  `OrderDiscount` decimal(18,3) DEFAULT NULL COMMENT '�����Ż�',
  `ReceivableAmount` decimal(18,3) DEFAULT NULL COMMENT 'Ӧ�ս��',
  `UncollectedeAmount` decimal(18,3) DEFAULT NULL COMMENT 'δ�ս��',
  `RealAmount` decimal(18,3) DEFAULT NULL COMMENT 'ʵ�ս��',
  `Freight` decimal(18,3) DEFAULT NULL COMMENT '�˷�',
  `LogisticsID` int(10) DEFAULT NULL COMMENT '������˾ID',
  `ExpressID` int(10) DEFAULT NULL COMMENT '��ݹ�˾ID',
  `ExpectedDeliDate` datetime DEFAULT NULL COMMENT '��������ʱ��',
  `SinceSome` varchar(100) DEFAULT NULL COMMENT '�����',
  `PaymentMethod` int(11) DEFAULT NULL COMMENT '���ʽ 0:����֧�� 1���������� ö������',
  `PaytDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `TradingNumber` varchar(100) DEFAULT NULL COMMENT '���׺�',
  `PaymentAccount` varchar(100) DEFAULT NULL COMMENT '�����˺�',
  `BuyCodFee` decimal(18,3) DEFAULT NULL COMMENT '��һ�����������',
  `GenerateOrderDate` datetime DEFAULT NULL COMMENT '��������ʱ��',
  `OrderProcess` varchar(500) DEFAULT NULL COMMENT '������������',
  `CancelPort` int(1) DEFAULT NULL COMMENT 'ȡ�������˿ڣ���ֿ�˻��߹���� 0:����� 1���ֿ��',
  `CancelRemark` varchar(200) DEFAULT NULL COMMENT '����ȡ����ע',
  `CancelDate` datetime DEFAULT NULL COMMENT '����ȡ��ʱ��',
  `IsSplitOrder` int(1) DEFAULT NULL COMMENT '�Ƿ��ֵĶ��� 0���� 1����',
  `SplitMasterOrder` varchar(100) DEFAULT NULL COMMENT '��ֶ�������ERP����',
  `ProvinceID` int(10) DEFAULT NULL COMMENT '�ջ��˵�����ʡ��ID',
  `CityID` int(10) DEFAULT NULL COMMENT '�ջ��˵����ڳ���ID',
  `DistrictID` int(10) DEFAULT NULL COMMENT '�ջ��˵����ڵ���ID',
  `DeliveryDate` datetime DEFAULT NULL COMMENT '����ʱ�䣺�Ե�һ�η���Ϊ׼',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_ord_base_ErpOrderCode` (`ErpOrderCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `ord_item` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OutOrderCode` varchar(1000) DEFAULT NULL COMMENT '�ⲿ������',
  `ErpOrderCode` varchar(100) DEFAULT NULL COMMENT 'ϵͳ������',
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ���',
  `ShopID` int(10) DEFAULT NULL COMMENT '����ID',
  `ChildOrderCode` varchar(100) DEFAULT NULL COMMENT '�Ӷ�����',
  `OuterItemID` int(11) DEFAULT NULL COMMENT 'OuterItem����Ʒ��ID',
  `BrandID` int(10) DEFAULT NULL COMMENT 'ProductsBrand��ID',
  `BrandName` varchar(50) DEFAULT NULL COMMENT 'Ʒ������',
  `CategoryID` int(10) DEFAULT NULL COMMENT 'Category��ID',
  `CategoryName` varchar(50) DEFAULT NULL COMMENT '��������',
  `ProductsID` int(10) DEFAULT NULL COMMENT 'Products��ID',
  `ProductsName` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsCode` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsNo` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsNum` int(4) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsSkuID` int(10) DEFAULT NULL COMMENT '��ƷSKU��ID',
  `ProductsSkuCode` varchar(50) DEFAULT NULL COMMENT '��ƷSKU��',
  `ProductsWeight` decimal(18,3) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsSkuSaleprop` varchar(200) DEFAULT NULL COMMENT 'SKU������ֵ���磺������ɫ:��ɫ;�ֻ��ײ�:�ٷ�����',
  `SellingPrice` decimal(18,3) DEFAULT NULL COMMENT '��Ʒ���ۼ�',
  `CostPrice` decimal(18,3) DEFAULT NULL COMMENT '��Ʒ�ɱ���',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `ord_outer` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OutOrderCode` varchar(1000) DEFAULT NULL COMMENT '�ⲿ�����š��Ͷ�����Դ�����ֶι���Ψһֵ����',
  `ErpOrderCode` varchar(100) DEFAULT NULL COMMENT 'ϵͳ������',
  `OrderSource` int(4) DEFAULT NULL COMMENT '������Դ��ö��',
  `ShopID` int(10) DEFAULT NULL COMMENT '����ID',
  `BuyNickName` varchar(50) DEFAULT NULL COMMENT '����ǳ�',
  `BuyName` varchar(50) DEFAULT NULL COMMENT '�������',
  `BuyTel` varchar(50) DEFAULT NULL COMMENT '��ҵ绰',
  `BuyMTel` varchar(50) DEFAULT NULL COMMENT '����ֻ�',
  `BuyAddr` varchar(100) DEFAULT NULL COMMENT '��ҵ�ַ',
  `BuyPostCode` varchar(50) DEFAULT NULL COMMENT '����ʱ�',
  `BuyMessage` varchar(4000) DEFAULT NULL COMMENT '�������',
  `SellerNickName` varchar(50) DEFAULT NULL COMMENT '�����ǳ�',
  `SellerRemark` varchar(4000) DEFAULT NULL COMMENT '���ұ�ע',
  `BuyProvince` varchar(50) DEFAULT NULL COMMENT '�ջ��˵�����ʡ��',
  `BuyCity` varchar(50) DEFAULT NULL COMMENT '�ջ��˵����ڳ���',
  `BuyDistrict` varchar(50) DEFAULT NULL COMMENT '�ջ��˵����ڵ���',
  `Created` datetime DEFAULT NULL COMMENT 'ƽ̨���״���ʱ��',
  `Modified` datetime DEFAULT NULL COMMENT 'ƽ̨�����޸�ʱ��',
  `PayDate` datetime DEFAULT NULL COMMENT '����ʱ�䡣��ʽ:yyyy-MM-dd HH:mm:ss',
  `ShippingType` varchar(50) DEFAULT NULL COMMENT '��������ʱ��������ʽ���������ǰ��������ʽ�п��ܸı䣬��ϵͳ�������ֶ�һֱ���䣩����ѡֵ��free(���Ұ���),post(ƽ��),express(���),ems(EMS),virtual(���ⷢ��)��25(���ձش�)��26(ԤԼ����)��',
  `TradeType` varchar(50) DEFAULT NULL COMMENT '���������б�ͬʱ��ѯ���ֽ������Ϳ��ö��ŷָ���Ĭ��ͬʱ��ѯguarantee_trade, auto_delivery, ec, cod��4�ֽ������͵����� ��ѡֵ fixed(һ�ڼ�) auction(����) guarantee_trade(һ�ڼۡ�����) auto_delivery(�Զ�����) independent_simple_trade(�������Ű潻��) independent_shop_trade(�����׼�潻��) ec(ֱ��) cod(��������) fenxiao(����) game_equi',
  `OrderAmount` decimal(18,3) DEFAULT NULL COMMENT '�������',
  `OrderDiscount` decimal(18,3) DEFAULT NULL COMMENT '�����Ż�',
  `ReceivableAmount` decimal(18,3) DEFAULT NULL COMMENT 'Ӧ�ս��',
  `UncollectedeAmount` decimal(18,3) DEFAULT NULL COMMENT 'δ�ս��',
  `RealAmount` decimal(18,3) DEFAULT NULL COMMENT 'ʵ�ս��',
  `Freight` decimal(18,3) DEFAULT NULL COMMENT '�ʷ�',
  `BuyCodFee` decimal(18,3) DEFAULT NULL COMMENT '��һ�����������',
  `BuyAccount` varchar(100) DEFAULT NULL COMMENT '��Ҹ����˺�',
  `SellerAccount` varchar(100) DEFAULT NULL COMMENT '�����տ��˺�',
  `IsNeedInvoice` int(1) DEFAULT NULL COMMENT '�Ƿ��з�Ʊ��Ϣ',
  `InvoiceInfo` varchar(2000) DEFAULT NULL COMMENT '��Ʊ��Ϣ',
  `IsHang` int(1) DEFAULT NULL COMMENT '�Ƿ���� 0���� 1����',
  `HangRemark` varchar(200) DEFAULT NULL COMMENT '����ע',
  `IsFromTbFxpt` int(1) DEFAULT NULL COMMENT '�Ƿ������Ա�����ƽ̨ 0:�� 1����',
  `OrderStatus` varchar(50) DEFAULT NULL COMMENT 'ƽ̨����״̬',
  `IsDownFin` int(1) DEFAULT NULL COMMENT '�Ƿ��������',
  `IsMergeOrder` int(1) DEFAULT NULL COMMENT '�Ƿ�ϲ��Ķ��� 0���� 1����',
  `MergeMasterOrder` varchar(50) DEFAULT NULL COMMENT '�ϲ����������ⲿ����',
  `IsSplitOrder` int(1) DEFAULT NULL COMMENT '�Ƿ��ֶ��� 0���� 1����',
  `SplitMasterOrder` varchar(50) DEFAULT NULL COMMENT '��ֶ��������ⲿ����',
  `SingleOutOrderCode` varchar(50) DEFAULT NULL COMMENT '�ⲿ�����ţ�û�кϲ��������ֶ���ʱ��OutOrderCode����һ��',
  `CanSplitMerge` int(1) DEFAULT NULL COMMENT '�ɲ�ɺ�=0�����ɲ𲻿ɺ�=1�����ɲ�ɺϲ�=2���ɲ𲻿ɺ�=3��',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_ord_outer_OutOrderCode_OrderSource` (`OutOrderCode`(200),`OrderSource`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `ord_outerItem` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `OutOrderCode` varchar(50) DEFAULT NULL COMMENT '�ⲿ�����š����Ӷ����������ֶι���Ψһֵ����',
  `ErpOrderCode` varchar(100) DEFAULT NULL COMMENT 'ϵͳ������',
  `ShopID` int(10) DEFAULT NULL COMMENT '����ID',
  `ChildOrderCode` varchar(50) DEFAULT NULL COMMENT '�Ӷ�����',
  `ProductsName` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `PicPath` varchar(500) DEFAULT NULL COMMENT '��ƷͼƬ�ľ���·��',
  `ProductsCode` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsNum` int(4) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsSkuCode` varchar(50) DEFAULT NULL COMMENT '��ƷSKU��',
  `ProductsSkuSaleprop` varchar(200) DEFAULT NULL COMMENT 'SKU������ֵ���磺������ɫ:��ɫ;�ֻ��ײ�:�ٷ�����',
  `AdjustFee` decimal(18,3) DEFAULT NULL COMMENT '�Ӷ����ֹ��������',
  `DiscountFee` decimal(18,3) DEFAULT NULL COMMENT '�Ӷ����������Żݽ��',
  `Payment` decimal(18,3) DEFAULT NULL COMMENT '�Ӷ���ʵ������ȷ��2λС������λ:Ԫ����:200.07����ʾ:200Ԫ7�֡����ڶ��Ӷ����Ľ��ף����㹫ʽ���£�payment = price * num + adjust_fee - discount_fee �����Ӷ������ף�payment����������paymentһ�£������˿�ɹ����Ӷ������������������Żݷ�̯������ɸ��ֶο��ܲ�Ϊ0.00Ԫ������ʹ���˿�ǰ��ʵ������ȥ�˿�е�ʵ���˿�����㡣',
  `Price` decimal(18,3) DEFAULT NULL COMMENT '��Ʒ�۸�',
  `DivideOrderFee` decimal(18,3) DEFAULT NULL COMMENT '��̯֮���ʵ�����',
  `IsProductAddFin` int(1) DEFAULT NULL COMMENT '�Ƿ���ӳɹ� 0���� 1����',
  `ProductAddMsg` varchar(50) DEFAULT NULL COMMENT '��Ʒ�����Ϣ',
  `RefundStatus` varchar(50) DEFAULT NULL COMMENT '�˿�״̬',
  `OrderStatus` varchar(50) DEFAULT NULL COMMENT '����״̬',
  `SingleOutOrderCode` varchar(50) DEFAULT NULL COMMENT '�ⲿ�����ţ�û�кϲ��������ֶ���ʱ��OutOrderCode����һ��',
  `OuterInfoID` int(10) DEFAULT NULL COMMENT 'Ord_Outer����ID',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_ord_outerItem_OutOrderCode_ChildOrderCode` (`OutOrderCode`,`ChildOrderCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `ord_refund` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BillNo` varchar(50) DEFAULT NULL COMMENT '�ۺ󵥱�� ϵͳ����Ψһ',
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ���',
  `OrderSource` int(4) DEFAULT NULL COMMENT '������Դ ö��',
  `ShopID` int(10) DEFAULT NULL COMMENT '����ID',
  `Status` varchar(50) DEFAULT NULL COMMENT '�ۺ�״̬ �����=0 ���˻�=1 ���ջ�=2 ���ջ�=3 ��ȡ��=4 ö��',
  `ErpOrderCode` varchar(100) DEFAULT NULL COMMENT 'ϵͳ������',
  `OutOrderCode` varchar(1000) DEFAULT NULL COMMENT '�ⲿ������',
  `Duty` int(4) DEFAULT NULL COMMENT '�ۺ����η�������˵�˿ͻ��߳��� ��ö��',
  `DutyOther` varchar(50) DEFAULT NULL COMMENT '�������η� �ı�',
  `AuditDate` datetime DEFAULT NULL COMMENT '���ʱ��',
  `AuditRemark` varchar(500) DEFAULT NULL COMMENT '��˱�ע',
  `ExpressCompany` varchar(50) DEFAULT NULL COMMENT '��Ʒ�ĻصĿ�ݹ�˾',
  `WayBillNo` varchar(50) DEFAULT NULL COMMENT '��Ʒ�Ļ��˵���',
  `SendBackDate` datetime DEFAULT NULL COMMENT '��Ʒ�Ļ�ʱ��',
  `RefundAmount` decimal(18,3) DEFAULT NULL COMMENT '��Ʒ�˿���',
  `RefundFreight` decimal(18,3) DEFAULT NULL COMMENT '���˷�',
  `Remark` varchar(2000) DEFAULT NULL COMMENT '�ۺ�ԭ��',
  `IsStockIn` int(1) DEFAULT NULL COMMENT '�ۺ���Ʒ�Ƿ���� 0���� 1����',
  `IsReceiveProduct` int(1) DEFAULT NULL COMMENT '�Ƿ��յ��ۺ���Ʒ 0���� 1����',
  `IsProductsOk` int(1) DEFAULT NULL COMMENT '��Ʒ�Ƿ�û���� 0���� 1����',
  `ReceiveRemark` varchar(500) DEFAULT NULL COMMENT '�ջ���ע',
  `ReceiveDate` datetime DEFAULT NULL COMMENT '�ջ�ʱ��',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `ord_refundItem` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BillNo` varchar(50) DEFAULT NULL COMMENT '�ۺ󵥺�',
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ���',
  `ShopID` int(10) DEFAULT NULL COMMENT '����ID',
  `ErpOrderCode` varchar(100) DEFAULT NULL COMMENT 'ϵͳ������',
  `OutOrderCode` varchar(1000) DEFAULT NULL COMMENT '�ⲿ������',
  `OrdItemID` int(10) DEFAULT NULL COMMENT 'Ord_Item������Ʒ��ID',
  `BrandID` int(10) DEFAULT NULL COMMENT 'ProductsBrand��ID',
  `BrandName` varchar(50) DEFAULT NULL COMMENT 'Ʒ������',
  `CategoryID` int(10) DEFAULT NULL COMMENT 'Category��ID',
  `CategoryName` varchar(50) DEFAULT NULL COMMENT '��������',
  `ProductsID` int(10) DEFAULT NULL COMMENT 'Products��ID',
  `ProductsCode` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsName` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsNo` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsWeight` decimal(18,3) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsSkuID` int(10) DEFAULT NULL COMMENT '��ƷSKU��ID',
  `ProductsSkuCode` varchar(50) DEFAULT NULL COMMENT 'SKU��',
  `ProductsSkuSaleprop` varchar(200) DEFAULT NULL COMMENT '��������(��ɫ����ɫ ���S)',
  `ProductsNum` int(4) DEFAULT NULL COMMENT '��Ʒ��������',
  `RefundNum` int(4) DEFAULT NULL COMMENT '��Ʒ�ۺ�����',
  `ShopPrice` decimal(18,3) DEFAULT NULL COMMENT '��Ʒ���۽��',
  `RefundPrice` decimal(18,3) DEFAULT NULL COMMENT '��Ʒ�˿���',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `products` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `No` varchar(50) NOT NULL COMMENT '��Ʒ���ţ�����Ϊ�գ��Զ���',
  `Code` varchar(50) NOT NULL COMMENT '��Ʒ���룺����Ϊ�գ������ظ����Զ��塣һ����Ʒ�����Ӧһ����Ʒ������Ʒû�й��ʱ����Ʒ������Ϊ��ƷSKU��',
  `BarCode` varchar(50) DEFAULT NULL COMMENT '��Ʒ���룺��Ϊ�գ������ظ���������Ʒ��׼���룬Ҳ���Զ��塣��Ӧ��Ʒ����ɨ����Ʒ����ȷ�϶�Ӧ��Ʒ�����ҡ�У�顢�����ȣ�',
  `BrandID` int(10) NOT NULL COMMENT '��ƷƷ��ID',
  `CategoryID` int(10) NOT NULL COMMENT '��Ʒ����ID',
  `ShelfLife` int(10) NOT NULL COMMENT '�����ڣ��죩 ',
  `Name` varchar(50) NOT NULL COMMENT '��Ʒ���ƣ�����Ϊ��',
  `SaleType` int(4) NOT NULL COMMENT '��Ʒ���ͣ����ۡ����ϣ���ѡ������Ϊ����ʱ�������ϼ�����  ö�����͡�����=1 ����=2  ����λ����\r\n����http://www.cnblogs.com/zgqys1980/archive/2010/05/31/1748404.html\r\n',
  `Weight` decimal(18,3) NOT NULL COMMENT '��Ʒ����',
  `CostPrice` decimal(18,3) NOT NULL COMMENT '��Ʒ�ɱ���',
  `SellingPrice` decimal(18,3) NOT NULL COMMENT '��Ʒ���ۼ�',
  `TaxRate` decimal(18,3) NOT NULL COMMENT '��Ʒ˰�ʣ� ����',
  `MeasurementUnitID` varchar(50) DEFAULT NULL COMMENT '��λID �ֵ��ά��',
  `SmallPic` varchar(200) DEFAULT NULL COMMENT '��ƷСͼ',
  `CreatePerson` varchar(50) NOT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  `Remark` text COMMENT '��ע',
  `Status` int(4) NOT NULL COMMENT '������=1   �ֿ���=2 ö������',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_products_Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT=' ��Ʒ��Ϣ��'
;
INSERT products (No,Code,BrandID,CategoryID,ShelfLife,Name,SaleType,Weight,CostPrice,SellingPrice,TaxRate,CreatePerson,CreateDate,UpdatePerson,UpdateDate,Status) VALUES ( 'test02','test02',0,0,0,'test02',3,1500.000,0.000,0.000,0.100,'sheng.hao','2015/9/24 11:36:01','sheng.hao','2015/9/24 12:34:36',2);

CREATE TABLE `productsMaterialMap` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `SoureProductsSkuCode` varchar(50) DEFAULT NULL COMMENT '��Ʒsku���� ���ӣ���ƻ����Ʒ',
  `FromProductsSkuCode` varchar(50) DEFAULT NULL COMMENT '�����õ� ��Ʒsku����  ���ø�ƻ����Ʒ���   ',
  `FromNum` int(10) DEFAULT NULL COMMENT '���õ����� Ĭ����1  ����������4 ',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��Ʒ-���Ϲ����� '
;

CREATE TABLE `productsSku` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `Code` varchar(50) NOT NULL COMMENT '��ƷSKU�룺����Ϊ�գ������ظ����Զ��塣��Ӧ��С��浥λ����ɨ����ƷSKU��ȷ�϶�Ӧ��Ʒ�����ҡ�У�顢�����ȣ�',
  `Saleprop` varchar(50) DEFAULT NULL COMMENT '��Ʒ�������ԣ�һ����Ʒ���ܻ���Ϊ��ͬ���������ԣ���Ϊ���SKU���ַ�����ʽ���棬����ÿһ��������:����ֵ��Ϊһ�ԣ�ÿ������֮��ʹ�ð�Ƿֺš�;���ָ��� \r\n      ��  ��ɫ:��ɫ;���:M;�ײ�:A���ײ�   �� һ����Ʒ���Զ�Ӧ����������ԣ�ÿ���������Զ�Ӧһ����ƷSKU�롣\r\n�ı������û��Լ�¼��\r\n',
  `BarCode` varchar(50) DEFAULT NULL COMMENT '��Ʒsku���룺��Ϊ�գ������ظ���������Ʒ��׼���룬Ҳ���Զ��塣��Ӧ���յ�Ʒ����ɨ����Ʒ����ȷ�϶�Ӧ��Ʒ�����ҡ�У�顢�����ȣ�',
  `ProductsID` int(10) NOT NULL COMMENT '��Ʒ���ʶ',
  `ProductsCode` varchar(50) NOT NULL COMMENT '��Ʒ���� ������Ʒ��',
  `Weight` decimal(18,3) NOT NULL COMMENT '��Ʒsku����',
  `CostPrice` decimal(18,3) NOT NULL COMMENT '��Ʒ�ɱ��ۣ� ��ӵ�ʱ�� Ĭ�ϼ�����Ʒ��Ӧ�ļ۸�',
  `SellingPrice` decimal(18,3) NOT NULL COMMENT '��Ʒ���ۼۣ�\r\n��ӵ�ʱ�� Ĭ�ϼ�����Ʒ��Ӧ�ļ۸�\r\n',
  `CreatePerson` varchar(50) NOT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_productsSku_Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��Ʒsku����Ϣ�� '
;
INSERT productsSku (Code,Saleprop,ProductsID,ProductsCode,Weight,CostPrice,SellingPrice,CreatePerson,CreateDate,UpdatePerson,UpdateDate) VALUES ( 'test0101','��ɫ����ɫ,���룺40',1,'test02',0.000,0.000,0.000,'sheng.hao','2015/9/24 11:36:01','sheng.hao','2015/9/24 12:34:36');
INSERT productsSku (Code,Saleprop,ProductsID,ProductsCode,Weight,CostPrice,SellingPrice,CreatePerson,CreateDate,UpdateDate) VALUES ( 'test0102','��ɫ����ɫ,���룺41',1,'test01',0.000,0.000,0.000,'sheng.hao','2015/9/23 17:10:58','0001/1/1 0:00:00');
INSERT productsSku (Code,Saleprop,ProductsID,ProductsCode,Weight,CostPrice,SellingPrice,CreatePerson,CreateDate,UpdateDate) VALUES ( 'test0103','��ɫ����ɫ,���룺42',1,'test01',0.000,0.000,0.000,'sheng.hao','2015/9/23 17:10:58','0001/1/1 0:00:00');

CREATE TABLE `shop` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `Code` varchar(50) NOT NULL COMMENT '���̱��� Ψһֵ',
  `Name` varchar(50) DEFAULT NULL COMMENT '��������',
  `Type` varchar(50) NOT NULL COMMENT '��������code(���ꡢ�������ŵ꣩',
  `PlatformType` varchar(50) NOT NULL COMMENT 'ƽ̨���� code(�Ա���������)',
  `StoreAddr` varchar(100) DEFAULT NULL COMMENT '�ŵ��ַ ��ʡ���� �ֵ���',
  `Longitude` varchar(50) DEFAULT NULL COMMENT '����',
  `Latitude` varchar(50) DEFAULT NULL COMMENT 'γ��',
  `AppKey` varchar(50) DEFAULT NULL COMMENT 'AppKey',
  `AppSecret` varchar(100) DEFAULT NULL COMMENT 'AppSecret',
  `AppSession` varchar(100) DEFAULT NULL COMMENT 'AppSession',
  `RefreshToken` varchar(100) DEFAULT NULL COMMENT 'RefreshToken',
  `ContactPerson` varchar(50) DEFAULT NULL COMMENT '��ϵ��',
  `ContactTel` varchar(50) DEFAULT NULL COMMENT '��ϵ�˵绰',
  `Website` varchar(50) DEFAULT NULL COMMENT '��ַ',
  `Remark` varchar(500) DEFAULT NULL COMMENT '��ע',
  `IsEnable` int(1) DEFAULT NULL COMMENT '�Ƿ���ã��ǣ���',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_shop_Code` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='������Ϣ��'
;

CREATE TABLE `shopAllocation` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `ShopID` int(10) NOT NULL COMMENT '���̱��ʶ',
  `ProductsID` int(10) NOT NULL COMMENT '��Ʒ���ʶ',
  `ProductsSkuID` int(10) NOT NULL COMMENT '��ƷSku���ʶ',
  `SaleInventory` int(10) NOT NULL COMMENT '���ۿ��',
  `IsSalePub` int(1) NOT NULL COMMENT '�Ƿ����۹����� 0�� 1��',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_shopAllocation_ShopID_ProductsSkuID` (`ShopID`,`ProductsSkuID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='���̿������'
;

CREATE TABLE `shopProducts` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `ShopID` int(10) NOT NULL COMMENT '���̱��ʶ',
  `PlatformType` int(4) NOT NULL COMMENT 'ƽ̨���� ö��',
  `ProductsID` int(10) NOT NULL COMMENT '��Ʒ���ʶ ����0��ʾ����ɹ�',
  `GoodsId` int(10) NOT NULL COMMENT 'ƽ̨��ƷID',
  `ProNo` varchar(50) NOT NULL COMMENT 'ƽ̨��Ʒ����',
  `ProTitle` varchar(150) NOT NULL COMMENT 'ƽ̨��Ʒ����',
  `MarketPrice` decimal(18,3) NOT NULL COMMENT 'ƽ̨�г���',
  `ImgUrl` varchar(1200) DEFAULT NULL COMMENT 'ƽ̨��ƷͼƬ��ַ',
  `Price` decimal(18,3) NOT NULL COMMENT 'ƽ̨����',
  `CateId` int(10) NOT NULL COMMENT 'ƽ̨ϵͳ�ϼ���ĿID',
  `CustomCateId` varchar(50) DEFAULT NULL COMMENT '�̼��Զ������ID�����IDֵ��Ӣ�İ�Ƕ��Ÿ�����ֵ����Ϊ�գ�',
  `ProductKucList` varchar(4000) DEFAULT NULL COMMENT 'Sku��������json�ַ���',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_shopProducts_ShopID_ProNo` (`ShopID`,`ProNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='������Ʒ��Ϣ��'
;

CREATE TABLE `student` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����',
  `SstuNmae` varchar(20) DEFAULT NULL COMMENT '����',
  `Sex` char(1) DEFAULT NULL COMMENT '�Ա�',
  `ClassId` int(10) DEFAULT NULL COMMENT '�༶id',
  `CreTime` datetime DEFAULT NULL COMMENT '��ѧʱ��',
  `IsTuanYuan` char(1) DEFAULT NULL COMMENT '�Ƿ���Ա',
  `Score` decimal(18,3) DEFAULT NULL COMMENT '�ɼ�',
  `Remark` text COMMENT '��ע',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8
;
INSERT student (SstuNmae,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'conan',4,'2015/9/9 0:00:00','1',100.000);
INSERT student (SstuNmae,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'iuiu',3,'2015/9/9 0:00:00','1',0.000);
INSERT student (SstuNmae,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'iuiuui',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'uiiuiuui',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'iuuiui',4,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,ClassId,CreTime,IsTuanYuan,Score) VALUES ( '�ϸ�ϸ�ϸ�',4,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( '������','0',5,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'ʢ��','0',5,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( '����','0',6,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'a','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'b','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'c','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'd','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'e','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'f','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'g','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'h','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'i','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'j','0',3,'2015/9/9 0:00:00','0',0.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'l','0',3,'2015/9/9 0:00:00','0',49.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'm','0',3,'2015/9/9 0:00:00','0',50.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'n','0',3,'2015/9/9 0:00:00','0',51.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'o','0',3,'2015/9/9 0:00:00','0',52.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'p','0',3,'2015/9/9 0:00:00','0',53.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'q','0',3,'2015/9/9 0:00:00','0',54.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'r','0',3,'2015/9/9 0:00:00','0',55.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 's','0',3,'2015/9/9 0:00:00','0',56.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 't','0',3,'2015/9/9 0:00:00','0',57.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'u','0',3,'2015/9/9 0:00:00','0',58.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'v','0',3,'2015/9/9 0:00:00','0',59.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'w','0',3,'2015/9/9 0:00:00','0',60.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'x','0',3,'2015/9/9 0:00:00','0',61.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'y','0',3,'2015/9/9 0:00:00','0',62.000);
INSERT student (SstuNmae,Sex,ClassId,CreTime,IsTuanYuan,Score) VALUES ( 'z','0',3,'2015/9/9 0:00:00','0',63.000);
INSERT student (SstuNmae,ClassId,CreTime,Score) VALUES ( '��uuuuuuuuuuuuuu',0,'2015/9/10 0:00:00',5.014);
INSERT student (SstuNmae,ClassId,CreTime,Score) VALUES ( '�͹��Һͼƻ�',0,'2015/9/10 0:00:00',0.000);
INSERT student () VALUES ( );

CREATE TABLE `supp_merchants` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `MerchantsCode` varchar(50) DEFAULT NULL COMMENT '��Ӧ�̴��� Ψһֵ',
  `MerchantsName` varchar(50) DEFAULT NULL COMMENT '��Ӧ������',
  `ChargePerson` varchar(50) DEFAULT NULL COMMENT '��Ӧ�̸�����',
  `ChargeTel` varchar(50) DEFAULT NULL COMMENT '�����˵绰',
  `EMail` varchar(50) DEFAULT NULL COMMENT '��Ӧ������',
  `ContactPerson` varchar(50) DEFAULT NULL COMMENT '��ϵ��',
  `ContactTel` varchar(50) DEFAULT NULL COMMENT '��ϵ�˵绰',
  `ContactAddr` varchar(100) DEFAULT NULL COMMENT '��ϵ��ַ',
  `ContactPostCode` varchar(50) DEFAULT NULL COMMENT '�ʱ�',
  `ContactFax` varchar(50) DEFAULT NULL COMMENT '����',
  `Website` varchar(50) DEFAULT NULL COMMENT '��ַ',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  `Remark` varchar(500) DEFAULT NULL COMMENT '��ע',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_supp_merchants_MerchantsCode` (`MerchantsCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `supp_merchantsProList` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `MerchantsCode` varchar(50) DEFAULT NULL COMMENT '��Ӧ�̴���',
  `MerchantsName` varchar(50) DEFAULT NULL COMMENT '��Ӧ������',
  `ProductCode` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductName` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductSkuID` int(10) DEFAULT NULL COMMENT '��Ʒ�����ƷSKU�� ��',
  `ProductSkuProPerty` varchar(200) DEFAULT NULL COMMENT '��Ʒ�������ԣ���ɫ:��ɫ;���:M;�ײ�:A���ײͣ�',
  `MeasurementUnit` varchar(50) DEFAULT NULL COMMENT '��λ��������֣� sys_code �ֵ��ά��',
  `UnitPrice` decimal(18,3) DEFAULT NULL COMMENT '����',
  `SupplyCycle` varchar(50) DEFAULT NULL COMMENT '�������� �ı����� �û��ֶ���д ',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  `Remark` varchar(500) DEFAULT NULL COMMENT '��ע',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `sys_area` (
  `ID` int(10) NOT NULL,
  `Name` varchar(200) DEFAULT NULL COMMENT '��������',
  `ParentID` int(10) DEFAULT NULL COMMENT '����ID ����Ϊ0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='ʡ�����ֵ��� '
;

CREATE TABLE `sys_billno` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `TypeNo` varchar(10) DEFAULT NULL COMMENT '���ǰ׺ ���磺XSC',
  `BillNo` varchar(50) DEFAULT NULL COMMENT '���ݱ�� Ψһ',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_billNo_TypeNo_BillNo` (`TypeNo`,`BillNo`)
) ENGINE=InnoDB AUTO_INCREMENT=2001 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='���ݱ�ű�'
;
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636094385','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636096556','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636096956','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636097356','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636097756','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636098156','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636098556','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636098996','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636099686','2015/9/21 16:36:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636100186','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636100666','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636101106','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636101516','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636102006','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636102396','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636102846','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636103246','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636103646','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636104046','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636104446','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636104836','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636105246','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636105726','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636106126','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636106516','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636106926','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636107326','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636107726','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636108126','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636108516','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636108926','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636109486','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636109956','2015/9/21 16:36:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636110356','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636110756','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636111156','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636111526','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636111886','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636112246','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636112606','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636112966','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636113326','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636113686','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636114047','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636114407','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636114807','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636115207','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636115607','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636116007','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636116407','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636116807','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636117207','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636117607','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636118007','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636118407','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636118807','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636119207','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636119607','2015/9/21 16:36:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636120007','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636120447','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636120887','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636121207','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636121567','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636121927','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636122287','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636122647','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636123007','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636123367','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636123727','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636124167','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636124537','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636124897','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636125297','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636125697','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636126097','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636126497','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636126897','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636127297','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636127697','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636128097','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636128497','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636128897','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636129297','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636129697','2015/9/21 16:36:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636130097','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636130497','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636130897','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636131298','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636131658','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636132018','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636132378','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636132778','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636133138','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636133498','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636133858','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636134218','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636134578','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636135018','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636135378','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636135738','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636136138','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636136538','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636136938','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636137338','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636137738','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636138138','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636138538','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636138938','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636139338','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636139738','2015/9/21 16:36:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636140138','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636140538','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636140938','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636141338','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636141738','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636142098','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636142498','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636142938','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636143298','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636143658','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636144018','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636144378','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636144738','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636145178','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636145618','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636145988','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636146458','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636146868','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636147228','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636147628','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636148028','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636148428','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636148829','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636149229','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636149629','2015/9/21 16:36:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636150029','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636150429','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636150829','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636151269','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636151709','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636152109','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636152469','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636152829','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636153189','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636153549','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636153909','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636154269','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636154629','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636154989','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636155349','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636155709','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636156069','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636156429','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636156829','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636157229','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636157629','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636158029','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636158429','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636158829','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636159229','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636159789','2015/9/21 16:36:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636160309','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636160709','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636161189','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636161589','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636161989','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636162429','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636162869','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636163239','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636163599','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636163959','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636164319','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636164679','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636165039','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636165399','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636165759','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636166119','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636166520','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636166880','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636167280','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636167680','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636168080','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636168480','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636168880','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636169280','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636169680','2015/9/21 16:36:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636170080','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636170490','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636170880','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636171280','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636171720','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636172160','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636172600','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636173040','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636173480','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636173840','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636174210','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636174570','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636174930','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636175290','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636175650','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636176010','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636176370','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636176770','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636177130','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636177530','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636178410','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636178810','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636179210','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636179610','2015/9/21 16:36:17');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636180010','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636180410','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636180810','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636181210','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636181610','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636182010','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636182450','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636182890','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636183330','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636183771','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636184211','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636184651','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636185011','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636185371','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636185731','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636186091','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636186451','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636186811','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636187211','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636187571','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636187971','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636188371','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636188851','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636189251','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636189651','2015/9/21 16:36:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636190051','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636190451','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636191011','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636191451','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636191851','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636192251','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636192691','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636193131','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636193571','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636194011','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636194461','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636194901','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636195341','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636195781','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636196141','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636196501','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636196861','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636197221','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636198341','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636198781','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636199181','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636199621','2015/9/21 16:36:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636200181','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636200621','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636201021','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636201422','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636201822','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636202222','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636202622','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636203022','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636203422','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636203862','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636204302','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636204742','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636205182','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636205622','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636206062','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636206502','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636206942','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636207382','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636207742','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636208102','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636208462','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636208822','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636209222','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636209632','2015/9/21 16:36:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636210022','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636210432','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636210832','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636211232','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636211632','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636212032','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636212432','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636212832','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636213272','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636213712','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636214272','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636214712','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636215152','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636215632','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636216072','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636216512','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636216952','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636217392','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636217832','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636218272','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636218713','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636219073','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636219473','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636219913','2015/9/21 16:36:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636220313','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636220713','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636221113','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636221513','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636221913','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636222313','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636222713','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636223113','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636223513','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636223913','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636224353','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636224753','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636225193','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636225633','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636226073','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636226513','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636226953','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636227393','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636227833','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636228253','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636228653','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636229073','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636229513','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636229953','2015/9/21 16:36:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636230393','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636230793','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636231193','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636231593','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636232033','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636232473','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636232873','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636233273','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636233673','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636234073','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636234473','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636234883','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636235353','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636235773','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636236184','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636236594','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636237024','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636237424','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636237814','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636238224','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636238624','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636239014','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636239424','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636239824','2015/9/21 16:36:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636240304','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636240704','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636241104','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636241544','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636241984','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636242424','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636242864','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636243304','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636243744','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636244184','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636244624','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636245064','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636245504','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636245944','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636246504','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636246944','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636247384','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636247744','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636248144','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636248544','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636248944','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636249464','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636249904','2015/9/21 16:36:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636250384','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636250784','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636251224','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636251624','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636252024','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636252424','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636252824','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636253264','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636253665','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636254105','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636254545','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636254985','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636255505','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636255945','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636256385','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636256825','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636257265','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636257665','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636258025','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636258385','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636258825','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636259225','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636259625','2015/9/21 16:36:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636260025','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636260535','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636260945','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636261345','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636261745','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636262185','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636262585','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636262985','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636263385','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636263785','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636264185','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636264585','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636265105','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636265505','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636265945','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636266385','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636266825','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636267265','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636267705','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636268065','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636268425','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636268785','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636269225','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636269585','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636269945','2015/9/21 16:36:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636270305','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636270705','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636271105','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636271506','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636271906','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636272306','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636272706','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636273106','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636273516','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636273916','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636274306','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636274706','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636275116','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636275506','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636275906','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636276306','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636276746','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636277196','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636277636','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636278076','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636278516','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636278876','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636279236','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636279596','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636279956','2015/9/21 16:36:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636280316','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636280676','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636281076','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636281476','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636281876','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636282276','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636282676','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636283076','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636283476','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636283876','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636284276','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636284676','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636285076','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636285516','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636285916','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636286316','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636286756','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636287196','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636287636','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636288076','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636288476','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636288837','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636289197','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636289557','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636289917','2015/9/21 16:36:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636290277','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636290637','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636290997','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636291357','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636291717','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636292117','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636292517','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636292917','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636293317','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636293717','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636294127','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636294517','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636294957','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636295357','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636295757','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636296157','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636296557','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636296957','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636297357','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636297797','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636298197','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636298607','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636298957','2015/9/21 16:36:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636302307','2015/9/21 16:36:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636355140','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636355510','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636355870','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636356230','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636356590','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636356950','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636357510','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636358110','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636358510','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636358911','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636359311','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636359711','2015/9/21 16:36:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636360111','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636360511','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636360911','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636361311','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636361711','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636362151','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636362711','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636363271','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636363661','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636364261','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636364781','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636365141','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636365501','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636365861','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636366221','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636366671','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636367111','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636367681','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636368481','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636369281','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636369771','2015/9/21 16:36:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636370261','2015/9/21 16:36:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636370731','2015/9/21 16:36:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636371101','2015/9/21 16:36:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636372141','2015/9/21 16:36:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636372911','2015/9/21 16:36:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636373351','2015/9/21 16:36:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636374451','2015/9/21 16:36:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636381282','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636381722','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636382122','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636382522','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636382922','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636383322','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636383762','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636384162','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636384602','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636384962','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636385322','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636385682','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636386042','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636386402','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636386762','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636387122','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636387482','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636387842','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636388202','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636388602','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636389002','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636389402','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636389812','2015/9/21 16:36:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636390202','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636390602','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636391002','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636391402','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636391802','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636392282','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636392682','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636393082','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636393482','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636393893','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636394283','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636394683','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636395093','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636395443','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636395803','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636396173','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636396533','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636397013','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636397373','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636397733','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636398203','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636398693','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636399183','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636399573','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636399973','2015/9/21 16:36:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636400373','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636400773','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636401173','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636401573','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636401973','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636402733','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636403373','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636404063','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636404733','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636405213','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636405973','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636406733','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636407173','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636407613','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636408013','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636408613','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636409213','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636409613','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636409973','2015/9/21 16:36:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636410333','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636410693','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636411094','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636411454','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636411854','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636412254','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636412664','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636413054','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636413534','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636413934','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636414334','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636414744','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636415134','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636415534','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636416064','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636416584','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636417094','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636417774','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636418174','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636418534','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636419094','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636419454','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636419814','2015/9/21 16:36:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636420184','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636420534','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636420974','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636421374','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636421734','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636422104','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636422504','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636422904','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636423304','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636423704','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636424184','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636424704','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636425104','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636425504','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636425944','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636426424','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636426984','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636427624','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636428264','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636428825','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636429345','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636429865','2015/9/21 16:36:42');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636430225','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636430585','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636430945','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636431305','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636431665','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636432025','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636432385','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636432745','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636433105','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636433505','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636433905','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636434305','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636434705','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636435105','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636435505','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636435905','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636436305','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636436705','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636437105','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636437515','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636438025','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636438545','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636439105','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636439705','2015/9/21 16:36:43');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636440265','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636440665','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636441025','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636441385','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636441745','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636442105','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636442465','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636442825','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636443185','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636443785','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636444145','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636444545','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636444945','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636445345','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636445745','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636446386','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636446826','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636447226','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636447636','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636448026','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636448426','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636448906','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636449396','2015/9/21 16:36:44');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636450036','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636450636','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636451276','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636451916','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636452506','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636453196','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636453786','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636454146','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636454506','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636455186','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636455826','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636456186','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636456596','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636456986','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636457396','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636457796','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636458196','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636458596','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636458996','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636459396','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636459796','2015/9/21 16:36:45');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636460196','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636460596','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636461676','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636462156','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636462596','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636463036','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636463477','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636463917','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636464357','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636464797','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636465237','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636465597','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636466077','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636466437','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636466797','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636467157','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636467557','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636467957','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636468357','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636468877','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636469277','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636469677','2015/9/21 16:36:46');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636470077','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636470557','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636470957','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636471357','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636471757','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636472157','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636472557','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636472957','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636473397','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636473837','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636474277','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636474957','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636475397','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636475837','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636476277','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636476677','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636477037','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636477397','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636477757','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636478157','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636478557','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636478957','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636479357','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636479757','2015/9/21 16:36:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636480167','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636480567','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636480968','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636481368','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636481768','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636482168','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636482568','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636482968','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636483408','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636483848','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636484288','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636484728','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636485168','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636485608','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636486048','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636486488','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636486928','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636487368','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636487728','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636488168','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636488568','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636489008','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636489448','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636489848','2015/9/21 16:36:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636490328','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636490728','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636491128','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636491528','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636491928','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636492328','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636492728','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636493128','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636493528','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636493948','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636494368','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636494818','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636495248','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636495668','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636496088','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636496508','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636496988','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636497478','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636497908','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636498308','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636498799','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636499269','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636499769','2015/9/21 16:36:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636500209','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636500669','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636501089','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636501489','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636501969','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636502449','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636502929','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636503369','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636503849','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636504409','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636504889','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636505429','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636505989','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636506509','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636507029','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636507469','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636507909','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636508389','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636508789','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636509189','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636509589','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636509989','2015/9/21 16:36:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636510389','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636510869','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636511349','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636511789','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636512309','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636512909','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636513589','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636514229','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636514869','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636515509','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636516150','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636516710','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636517150','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636517590','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636518030','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636518390','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636518750','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636519110','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636519550','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636519950','2015/9/21 16:36:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636520390','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636520790','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636521190','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636521590','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636521990','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636522420','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636522820','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636523220','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636523620','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636524020','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636524420','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636524900','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636525380','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636525780','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636526180','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636526580','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636527020','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636527430','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636527790','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636528150','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636528510','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636528870','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636529230','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636529590','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636529950','2015/9/21 16:36:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636530310','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636530670','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636531110','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636531470','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636531910','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636532390','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636532790','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636533190','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636533591','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636533991','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636534391','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636534791','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636535191','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636535591','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636535991','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636536671','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636537111','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636537511','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636537871','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636538351','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636538711','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636539071','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636539431','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636539791','2015/9/21 16:36:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636540161','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636540511','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636540881','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636541241','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636541601','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636542001','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636542401','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636542801','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636543201','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636543601','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636544001','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636544401','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636544801','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636545201','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636545601','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636546041','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636546481','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636546921','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636547281','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636547641','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636548001','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636548361','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636548721','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636549081','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636549441','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636549811','2015/9/21 16:36:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636550171','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636550521','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636550892','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636551332','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636551772','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636552172','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636552732','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636553212','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636553612','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636554012','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636554412','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636554812','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636555292','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636555692','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636556092','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636556532','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636556972','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636557372','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636557732','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636558092','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636558452','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636558812','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636559182','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636559532','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636559892','2015/9/21 16:36:55');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636560252','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636560622','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636561012','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636561412','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636561822','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636562222','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636562622','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636563582','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636563942','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636564342','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636564742','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636565142','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636565542','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636565942','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636566382','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636566822','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636567262','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636567702','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636568102','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636568503','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636568873','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636569233','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636569583','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636569943','2015/9/21 16:36:56');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636570303','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636570663','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636571063','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636571463','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636571863','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636572263','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636572663','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636573063','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636573463','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636573873','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636574353','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636574753','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636575313','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636575833','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636576273','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636576713','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636577153','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636577593','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636578033','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636578393','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636578753','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636579123','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636579473','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636579833','2015/9/21 16:36:57');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636580233','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636580633','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636581033','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636581433','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636581833','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636582313','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636582753','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636583153','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636583553','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636583963','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636584353','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636584763','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636585203','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636585633','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636586074','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636586524','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636587004','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636587444','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636587884','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636588324','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636588724','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636589164','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636589564','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636589924','2015/9/21 16:36:58');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636590324','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636590684','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636591084','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636591484','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636591884','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636592284','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636592684','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636593084','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636593644','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636594114','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636594694','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636595384','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636596044','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636596424','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636596914','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636597434','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636597844','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636598504','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636598984','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211636599484','2015/9/21 16:36:59');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637000134','2015/9/21 16:37:00');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637001564','2015/9/21 16:37:00');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637007695','2015/9/21 16:37:00');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637008375','2015/9/21 16:37:00');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637009015','2015/9/21 16:37:00');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637009615','2015/9/21 16:37:00');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637010325','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637010965','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637011525','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637011885','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637012295','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637012695','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637013295','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637013695','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637014095','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637014495','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637014895','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637015295','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637015695','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637016095','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637016495','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637016935','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637017495','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637018055','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637018575','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637019215','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637019805','2015/9/21 16:37:01');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637020365','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637020926','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637021606','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637022246','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637022886','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637023446','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637023806','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637024166','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637024526','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637024886','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637025246','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637025766','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637026256','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637026656','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637027056','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637027496','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637027896','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637028376','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637028776','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637029256','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637029856','2015/9/21 16:37:02');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637030376','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637030936','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637031496','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637032176','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637032816','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637033336','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637033696','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637034056','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637034416','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637034776','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637035136','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637035536','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637035896','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637036296','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637036696','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637037096','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637037496','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637037896','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637038297','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637038697','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637039097','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637039507','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637039937','2015/9/21 16:37:03');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637040337','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637040817','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637041457','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637042017','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637042657','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637043257','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637043697','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637044097','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637044457','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637044817','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637045177','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637045777','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637046137','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637046497','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637046857','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637047257','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637047617','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637048017','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637048427','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637048817','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637049217','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637049617','2015/9/21 16:37:04');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637050027','2015/9/21 16:37:05');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637050417','2015/9/21 16:37:05');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637050827','2015/9/21 16:37:05');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637051227','2015/9/21 16:37:05');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637051747','2015/9/21 16:37:05');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637052187','2015/9/21 16:37:05');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637071758','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637072118','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637072478','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637072838','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637073208','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637073569','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637073929','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637074289','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637074649','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637075009','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637075409','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637075809','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637076209','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637076609','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637077009','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637077409','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637077809','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637078209','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637078609','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637079009','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637079489','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637079809','2015/9/21 16:37:07');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637080409','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637080819','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637081179','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637081539','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637081899','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637083059','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637083539','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637083899','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637084259','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637084619','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637085019','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637085419','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637085819','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637086219','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637086619','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637087019','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637087419','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637087819','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637088219','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637088619','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637089019','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637089499','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637089979','2015/9/21 16:37:08');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637090579','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637091100','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637091460','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637091820','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637092180','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637092540','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637092900','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637093270','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637093630','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637093990','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637094350','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637094710','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637095110','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637095510','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637095910','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637096310','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637096710','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637097110','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637097510','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637097910','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637098440','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637098870','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637099270','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637099670','2015/9/21 16:37:09');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637100270','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637100910','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637101550','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637102190','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637102590','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637102950','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637103310','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637103670','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637104030','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637104390','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637104750','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637105110','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637105510','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637105910','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637106310','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637106710','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637107110','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637107510','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637107920','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637108321','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637108721','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637109121','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637109511','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637109921','2015/9/21 16:37:10');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637110511','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637111161','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637111791','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637112431','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637113071','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637113711','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637114351','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637114871','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637115241','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637115591','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637115961','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637116321','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637116721','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637117121','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637117521','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637117921','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637118721','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637119211','2015/9/21 16:37:11');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637120041','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637120661','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637121261','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637121881','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637122481','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637123101','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637123721','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637124261','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637124801','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637125361','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637125822','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637126702','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637127282','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637127642','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637128322','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637128782','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637129242','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637129682','2015/9/21 16:37:12');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637130262','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637130722','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637131302','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637131942','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637132582','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637133102','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637133782','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637134422','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637134932','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637135332','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637135732','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637136132','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637136542','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637136932','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637137342','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637137742','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637138132','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637138542','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637138942','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637139342','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637139822','2015/9/21 16:37:13');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637140462','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637141102','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637141712','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637142262','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637142822','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637143373','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637143973','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637144613','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637145253','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637145853','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637146413','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637147053','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637147413','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637147883','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637148693','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637149723','2015/9/21 16:37:14');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637150583','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637151353','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637151753','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637152393','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637152883','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637153323','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637154123','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637154933','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637155693','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637156343','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637156833','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637157503','2015/9/21 16:37:15');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637166354','2015/9/21 16:37:16');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637183565','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637184845','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637185635','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637186045','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637186405','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637186765','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637187315','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637188005','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637188405','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637188805','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637189205','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637189605','2015/9/21 16:37:18');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637190005','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637190405','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637190925','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637191595','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637192405','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637192885','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637193285','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637193725','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637194765','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637195325','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637195886','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637196446','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637197006','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637197766','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637198246','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637198606','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637198966','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637199326','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637199686','2015/9/21 16:37:19');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637200046','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637200526','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637201086','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637201486','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637201886','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637202286','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637202686','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637203086','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637203486','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637203886','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637204286','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637204686','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637205086','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637205566','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637206166','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637206726','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637207286','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637207846','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637208486','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637209056','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637209606','2015/9/21 16:37:20');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637210166','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637210566','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637210926','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637211286','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637211646','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637212006','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637212366','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637212766','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637213167','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637213567','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637213967','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637214367','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637214767','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637215167','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637215567','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637215967','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637216377','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637216807','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637217287','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637217847','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637218497','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637219047','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637219767','2015/9/21 16:37:21');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637220327','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637220887','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637221397','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637221767','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637222127','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637222487','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637222847','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637223207','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637223717','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637224287','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637224687','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637225087','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637225497','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637225897','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637226297','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637226857','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637227337','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637227737','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637228137','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637228537','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637229097','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637229657','2015/9/21 16:37:22');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637230217','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637230768','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637231328','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637231888','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637232448','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637233008','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637233568','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637233968','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637234328','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637234688','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637235198','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637235728','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637236238','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637236768','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637237168','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637237568','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637237978','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637238378','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637238778','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637239178','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637239578','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637239978','2015/9/21 16:37:23');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637240378','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637240778','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637241738','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637242298','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637242858','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637243408','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637243968','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637244528','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637245018','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637245448','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637246018','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637246528','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637246888','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637247258','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637247608','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637248018','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637248419','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637248819','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637249219','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637249619','2015/9/21 16:37:24');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637250019','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637250419','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637251079','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637251579','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637251979','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637252379','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637252779','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637253219','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637253659','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637254219','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637254659','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637255099','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637255539','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637255979','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637256499','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637256979','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637257419','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637257779','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637258139','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637258539','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637258939','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637259339','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637259739','2015/9/21 16:37:25');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637260139','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637260539','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637260979','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637261499','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637261899','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637262299','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637262699','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637263099','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637263589','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637264029','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637264469','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637264909','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637265349','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637265790','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637266230','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637266750','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637267190','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637267630','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637267990','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637268550','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637268950','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637269350','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637269750','2015/9/21 16:37:26');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637270150','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637270550','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637270950','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637271350','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637271750','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637272330','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637272750','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637273150','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637273590','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637274030','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637274470','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637274910','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637275350','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637275790','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637276230','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637276670','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637277150','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637277590','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637278030','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637278470','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637278870','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637279240','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637279590','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637279990','2015/9/21 16:37:27');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637280390','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637280790','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637281190','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637281590','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637281990','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637282390','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637282790','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637283201','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637283601','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637284061','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637284461','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637284881','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637285301','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637285701','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637286261','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637286681','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637287121','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637287561','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637288001','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637288441','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637288881','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637289321','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637289681','2015/9/21 16:37:28');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637290121','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637290521','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637290981','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637291861','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637292641','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637293041','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637293441','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637293841','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637294241','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637294641','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637295041','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637295561','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637296141','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637297661','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637298141','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637298541','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637298941','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637299341','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637299761','2015/9/21 16:37:29');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637300201','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637300642','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637301082','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637301522','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637302122','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637302602','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637303002','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637303402','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637303802','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637304202','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637304602','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637305002','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637305442','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637305882','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637306322','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637306762','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637307162','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637307602','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637308182','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637308822','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637309302','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637309782','2015/9/21 16:37:30');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637310182','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637310582','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637310982','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637311382','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637311782','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637312182','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637312582','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637313022','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637313462','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637313882','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637314342','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637314762','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637315222','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637315702','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637316122','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637316562','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637317002','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637317442','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637317882','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637318323','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637318763','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637319203','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637319643','2015/9/21 16:37:31');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637320063','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637320463','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637320943','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637321423','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637321823','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637322223','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637322623','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637323023','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637323423','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637323823','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637324223','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637324663','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637325183','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637325833','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637326383','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637326943','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637327503','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637328063','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637328503','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637328943','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637329383','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637329823','2015/9/21 16:37:32');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637330263','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637330703','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637331103','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637331463','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637331823','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637332263','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637332703','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637333183','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637333583','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637333983','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637334383','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637334823','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637335223','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637335624','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637336064','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637336504','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637336904','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637337344','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637337784','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637338304','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637338744','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637339184','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637339624','2015/9/21 16:37:33');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637340064','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637340514','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637340944','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637341384','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637341824','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637342264','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637342704','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637343064','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637343424','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637343824','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637344264','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637344704','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637345104','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637345504','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637345914','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637346304','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637346704','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637347104','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637347504','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637347914','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637348304','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637348704','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637349114','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637349554','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637349994','2015/9/21 16:37:34');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637350424','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637350874','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637351314','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637351744','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637352194','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637352634','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637353065','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637353505','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637353875','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637354235','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637354675','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637355195','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637355635','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637356105','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637356515','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637356905','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637357305','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637357705','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637358105','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637358545','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637358955','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637359355','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637359755','2015/9/21 16:37:35');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637360145','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637360545','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637360945','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637361395','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637361835','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637362275','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637362795','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637363235','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637363675','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637364115','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637364555','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637364955','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637365315','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637365675','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637366035','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637366395','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637366755','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637367155','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637367555','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637367955','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637368395','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637368795','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637369195','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637369595','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637369995','2015/9/21 16:37:36');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637370395','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637370796','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637371196','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637371596','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637371996','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637372436','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637372876','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637373316','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637373756','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637374196','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637374636','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637375076','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637375516','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637375876','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637376236','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637376596','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637376956','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637377436','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637377836','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637378236','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637378636','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637379036','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637379436','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637379846','2015/9/21 16:37:37');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637380236','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637380636','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637381036','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637381436','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637381836','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637382236','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637382636','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637383076','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637383516','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637383956','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637384406','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637384846','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637385286','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637385726','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637386126','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637386486','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637386846','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637387206','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637387566','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637388007','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637388367','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637388767','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637389167','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637389567','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637389967','2015/9/21 16:37:38');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637390367','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637390767','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637391167','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637391647','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637392047','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637392447','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637392847','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637393247','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637393647','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637394047','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637394447','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637394887','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637395327','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637395767','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637396207','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637396727','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637397127','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637397487','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637397847','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637398207','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637398567','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637399727','2015/9/21 16:37:39');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637400127','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637400527','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637401007','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637401447','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637401847','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637402247','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637402647','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637403047','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637403447','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637403847','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637404247','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637404647','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637405047','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637405448','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637405848','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637406328','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637406768','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637407208','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637407648','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637408128','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637408528','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637408888','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637409248','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637409608','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637409968','2015/9/21 16:37:40');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637410328','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637410688','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637411048','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637411448','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637411848','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637412248','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637412648','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637413048','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637413448','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637413848','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637414248','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637414648','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637415088','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637415488','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637415888','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637416288','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637416688','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637417088','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637417528','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637417968','2015/9/21 16:37:41');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637472001','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637473081','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637473881','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637474671','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637475482','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637476042','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637476552','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637477322','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637478032','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637478642','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637479042','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637479442','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637479842','2015/9/21 16:37:47');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637480242','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637480642','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637481042','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637481442','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637482242','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637483072','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637483872','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637484512','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637485262','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637485952','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637486662','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637487072','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637487432','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637487792','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637488152','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637488512','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637488872','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637489232','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637489752','2015/9/21 16:37:48');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637490202','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637490702','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637491312','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637491792','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637492192','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637492592','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637492993','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637493393','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637493953','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637494353','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637494753','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637495153','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637495553','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637495953','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637496353','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637496873','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637497273','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637497913','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637498583','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637499433','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637499993','2015/9/21 16:37:49');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637500353','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637500713','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637501073','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637501433','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637501793','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637502153','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637502593','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637503033','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637503543','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637504143','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637504793','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637505193','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637505593','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637506003','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637506393','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637506833','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637507233','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637507633','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637508033','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637508433','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637508833','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637509243','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637509713','2015/9/21 16:37:50');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637510514','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637510914','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637511274','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637511634','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637511994','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637512354','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637512724','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637513074','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637513434','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637513794','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637514154','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637514514','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637514874','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637515274','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637515674','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637516074','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637516474','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637516874','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637517274','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637517674','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637518074','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637518794','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637519274','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637519684','2015/9/21 16:37:51');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637520114','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637520914','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637521674','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637522074','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637522434','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637522794','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637523154','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637523514','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637523874','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637524234','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637524594','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637524954','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637525314','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637525674','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637526034','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637526554','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637527044','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637527444','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637527845','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637528325','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637529115','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637529605','2015/9/21 16:37:52');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637530005','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637530405','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637530805','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637531205','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637531645','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637532195','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637532875','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637533435','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637533795','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637534155','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637534515','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637535315','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637535875','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637536235','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637536595','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637536955','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637537315','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637537675','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637538035','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637538395','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637538765','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637539165','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637539565','2015/9/21 16:37:53');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637540365','2015/9/21 16:37:54');
INSERT sys_billno (TypeNo,BillNo,CreateDate) VALUES ( 'XSC','1509211637540965','2015/9/21 16:37:54');

CREATE TABLE `sys_button` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Code` varchar(50) NOT NULL COMMENT '��ť���� Ψһ��',
  `Name` varchar(50) DEFAULT NULL COMMENT '��ť����',
  `Seq` int(4) DEFAULT '0' COMMENT '���� Ĭ��Ϊ0',
  `Description` varchar(50) DEFAULT NULL COMMENT '����',
  `Icon` varchar(50) DEFAULT NULL COMMENT '��ť��ʽ',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePeson` varchar(50) DEFAULT NULL COMMENT '������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `MenuCode` varchar(50) DEFAULT NULL COMMENT '�˵�����',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_button_Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='Ȩ�ް�ť��'
;
INSERT sys_button (Code,Seq) VALUES ( 'accredit
add
audit
browse
copy
delete
downlo',0);

CREATE TABLE `sys_code` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `Code` varchar(50) NOT NULL COMMENT '��� Ψһ��',
  `Text` varchar(50) DEFAULT NULL COMMENT '�ı�',
  `ParentCode` varchar(50) DEFAULT '0' COMMENT '�ϼ����Ĭ��0',
  `Seq` int(4) DEFAULT '0' COMMENT '����Ĭ��0',
  `IsEnable` int(1) DEFAULT NULL COMMENT '�Ƿ����� 1 �� 0 ��',
  `IsDefault` int(1) DEFAULT NULL COMMENT '�Ƿ�Ĭ�� 1 �� 0 ��',
  `Description` varchar(200) DEFAULT NULL COMMENT '����˵��',
  `CodeTypeName` varchar(50) DEFAULT NULL COMMENT '�����ֵ�����',
  `CodeType` varchar(50) DEFAULT NULL COMMENT '�����ֵ����',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_code_Code` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�����ֵ�'
;

CREATE TABLE `sys_codeType` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `Code` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '���ͱ��� Ψһ��',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '��������',
  `Description` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '����',
  `Seq` int(4) DEFAULT '0' COMMENT '���� Ĭ��0',
  `CreatePerson` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  `IsEnable` int(4) DEFAULT NULL COMMENT '�Ƿ���� 1��0 ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_codeType_Code` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�ֵ����'
;

CREATE TABLE `sys_colList` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `ViewName` varchar(50) DEFAULT NULL COMMENT '�б�����',
  `ViewCode` varchar(50) DEFAULT NULL COMMENT '�б���� ',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�б���Ϣ '
;

CREATE TABLE `sys_colPermissions` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `ColumnFild` varchar(50) DEFAULT NULL COMMENT '���ֶ�',
  `ColumnName` varchar(50) DEFAULT NULL COMMENT '������',
  `ViewCode` varchar(50) DEFAULT NULL COMMENT '�б����',
  `Seq` int(4) DEFAULT '0' COMMENT '���� Ĭ��0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�б��ֶ��б�'
;

CREATE TABLE `sys_log` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `UserCode` varchar(50) DEFAULT NULL COMMENT '�����ʺ�',
  `UserName` varchar(50) DEFAULT NULL COMMENT '������',
  `Position` varchar(50) DEFAULT NULL COMMENT 'λ��',
  `Target` varchar(20) DEFAULT NULL COMMENT '����',
  `Type` varchar(20) DEFAULT NULL COMMENT '���ͣ�1���� 2�����ⵥ�������� ö������',
  `OldMessage` varchar(2000) DEFAULT NULL COMMENT '�ɵ�ʵ���ַ���',
  `Date` datetime DEFAULT NULL COMMENT '����ʱ��',
  `ButtonName` varchar(50) DEFAULT NULL COMMENT '���� �¼����� �� �޸�',
  `Message` varchar(2000) DEFAULT NULL COMMENT '�޸ĺ��ʵ���ַ���',
  `Part1` varchar(100) DEFAULT NULL COMMENT '�����ֶ�1 �� ��type=1ʱ ���ֶδ洢�������ⲿ����',
  `Part2` varchar(100) DEFAULT NULL COMMENT '�����ֶ�2�� ��type=1ʱ ���ֶδ洢�������ڲ�����',
  `Part3` varchar(100) DEFAULT NULL COMMENT '�����ֶ�3 ͬ�� û�õ���Ϊ��',
  `ModeType` int(4) DEFAULT NULL COMMENT 'ģ��id(1 ����� 2 �ֿ�� )ö������',
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ����   ��ģ��id=2 �ֿ�ε�ʱ��������\r\n�������ֲ�ͬ�ֿ���û�\r\n',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='������־'
;

CREATE TABLE `sys_loginHistory` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `UserCode` varchar(50) NOT NULL COMMENT '�ʺ�',
  `UserName` varchar(50) NOT NULL COMMENT '�û���',
  `HostName` varchar(100) DEFAULT NULL COMMENT '������',
  `HostIP` varchar(20) DEFAULT NULL COMMENT '����ip',
  `LoginCity` varchar(50) DEFAULT NULL COMMENT '��¼����',
  `LoginDate` datetime DEFAULT NULL COMMENT '��¼ʱ��',
  `ModeType` int(4) DEFAULT NULL COMMENT 'ģ��id(1 ����� 2 �ֿ�� )ö������',
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ����   ��ģ��id=2 �ֿ�ε�ʱ��������',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��¼��־'
;
INSERT sys_loginHistory (UserCode,UserName,HostName,HostIP,LoginCity,LoginDate,ModeType) VALUES ( 'admin','����Ա','PAIXIE-PC','10.0.0.105','����ʡ������','2015/9/25 10:09:34',1);
INSERT sys_loginHistory (UserCode,UserName,HostName,HostIP,LoginCity,LoginDate,ModeType) VALUES ( 'admin','����Ա','PAIXIE-PC','10.0.0.105','����ʡ������','2015/9/25 15:58:40',1);
INSERT sys_loginHistory (UserCode,UserName,HostName,HostIP,LoginCity,LoginDate,ModeType) VALUES ( 'admin','����Ա','PAIXIE-PC','10.0.0.105','����ʡ������','2015/9/25 16:16:11',1);

CREATE TABLE `sys_menu` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `Code` varchar(50) NOT NULL COMMENT '�˵����� Ψһ��',
  `ParentCode` varchar(50) DEFAULT NULL COMMENT '��������',
  `Name` varchar(50) DEFAULT NULL COMMENT '�˵�����',
  `URL` varchar(50) DEFAULT NULL COMMENT '�˵���ַ',
  `IconClass` varchar(50) DEFAULT NULL COMMENT 'ͼ����ʽ',
  `IconURL` varchar(50) DEFAULT NULL COMMENT '��ʽ��ַ',
  `Seq` int(4) DEFAULT '0' COMMENT '���� Ĭ��0',
  `Description` varchar(200) DEFAULT NULL COMMENT '����',
  `IsVisible` int(1) DEFAULT NULL COMMENT '�Ƿ�ɼ� 1 �� 0 ��',
  `IsEnable` int(1) DEFAULT NULL COMMENT '�Ƿ���� 1 �� 0 ��',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  `ModeType` int(4) DEFAULT NULL COMMENT 'ģ��id(1 ����� 2 �ֿ�� )ö������',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_menu_Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�˵���'
;
INSERT sys_menu (Code,ParentCode,Name,Seq,IsVisible,IsEnable,ModeType) VALUES ( '001','0','��Ʒ����',0,1,1,1);
INSERT sys_menu (Code,ParentCode,Name,Seq,IsVisible,IsEnable,ModeType) VALUES ( '0011','001','��Ʒ�б�',0,1,1,1);
INSERT sys_menu (Code,ParentCode,Name,Seq,IsVisible,IsEnable,ModeType) VALUES ( '0012','001','��ƷƷ��',0,1,1,1);
INSERT sys_menu (Code,ParentCode,Name,Seq,IsVisible,IsEnable,ModeType) VALUES ( '0013','001','��Ʒ����',0,1,1,1);

CREATE TABLE `sys_menuButtonMap` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ButtonCode` varchar(50) NOT NULL COMMENT '�˵��¼����� �����ֶι���Ψһֵ����',
  `MenuCode` varchar(50) NOT NULL COMMENT '�˵����� �����ֶι���Ψһֵ����',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_menuButtonMap_ButtonCode_MenuCode` (`ButtonCode`,`MenuCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='�˵�-�˵��¼�������'
;

CREATE TABLE `sys_organize` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `Code` varchar(50) NOT NULL COMMENT '��֯���� Ψһ��',
  `ParentCode` varchar(50) DEFAULT NULL COMMENT '�������� ��߼�Ϊ0',
  `Seq` int(4) DEFAULT '0' COMMENT '���� Ĭ��0',
  `Name` varchar(50) DEFAULT NULL COMMENT '��֯����',
  `Description` varchar(200) DEFAULT NULL COMMENT '����',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_organize_Code` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��֯�ṹ'
;

CREATE TABLE `sys_organizeRoleMap` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RoleCode` varchar(50) NOT NULL COMMENT '��ɫ���� �����ֶι���Ψһֵ����',
  `OrganizeCode` varchar(50) NOT NULL COMMENT '��֯���� �����ֶι���Ψһֵ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_organizeRoleMap_RoleCode_OrganizeCode` (`RoleCode`,`OrganizeCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='��֯-��ɫ������'
;

CREATE TABLE `sys_role` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `Code` varchar(50) NOT NULL COMMENT '��ɫ���� Ψһ��',
  `Seq` int(4) DEFAULT '0' COMMENT '���� Ĭ�� 0',
  `Name` varchar(50) DEFAULT NULL COMMENT '��ɫ����',
  `Description` varchar(200) DEFAULT NULL COMMENT '����',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `ModeType` int(4) DEFAULT NULL COMMENT 'ģ��id(1 ����� 2 �ֿ�� )ö������',
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ����   ��ģ��id=2 �ֿ�ε�ʱ��������',
  `IsEnable` int(4) DEFAULT NULL COMMENT '�Ƿ���� 1��0 ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_role_Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='ϵͳ��ɫ'
;
INSERT sys_role (Code,Seq,Name,Description,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '001',0,'����Ա','����','admin','2015/9/25 8:39:42','admin','2015/9/25 8:39:42',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '002',0,'�ͷ�','admin','2015/9/25 8:40:09','admin','2015/9/25 8:40:09',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '343443',0,'4343','admin','2015/9/25 11:44:40','admin','2015/9/25 11:44:40',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '676776',0,'7667','admin','2015/9/25 11:45:58','admin','2015/9/25 11:45:58',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '656565',0,'656565','admin','2015/9/25 14:15:34','admin','2015/9/25 14:15:34',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '65656577',0,'7676','admin','2015/9/25 14:15:45','admin','2015/9/25 14:15:45',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '4545������',0,'uyjhnh','admin','2015/9/25 14:15:52','admin','2015/9/25 14:15:52',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '767676',0,'67767','admin','2015/9/25 14:15:58','admin','2015/9/25 14:15:58',1,1);
INSERT sys_role (Code,Seq,Name,Description,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '6576776',0,'767676','7676','admin','2015/9/25 14:16:04','admin','2015/9/25 14:16:04',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '766776',0,'767676','admin','2015/9/25 14:16:20','admin','2015/9/25 14:16:20',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( '767676uyyuuy',0,'uyuyyu','admin','2015/9/25 14:16:33','admin','2015/9/25 14:16:33',1,1);
INSERT sys_role (Code,Seq,Name,Description,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( 'uyuyuy',0,'uyuy','uyuy','admin','2015/9/25 14:16:39','admin','2015/9/25 14:16:39',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( 'uyuyuyfdfddf',0,'dffd','admin','2015/9/25 14:16:51','admin','2015/9/25 14:16:51',1,1);
INSERT sys_role (Code,Seq,Name,Description,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( 'uyyu',0,'uyuy','uyuy','admin','2015/9/25 14:16:57','admin','2015/9/25 14:16:57',1,1);
INSERT sys_role (Code,Seq,Name,Description,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( 'lklklk',0,'lklklk','lkkl','admin','2015/9/25 14:17:04','admin','2015/9/25 14:17:04',1,1);
INSERT sys_role (Code,Seq,Name,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( 'fdfdfd',0,'fdfdfd','admin','2015/9/25 14:17:12','admin','2015/9/25 14:17:12',1,1);
INSERT sys_role (Code,Seq,Name,Description,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType,IsEnable) VALUES ( 'fdfdfdfd',0,'fddffdfd','fddf','admin','2015/9/25 14:17:18','admin','2015/9/25 14:17:18',1,1);

CREATE TABLE `sys_roleColumnmap` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `RoleCode` varchar(58) DEFAULT NULL COMMENT '��ɫ����',
  `ColPermissionsID` int(10) DEFAULT NULL COMMENT '�ֶ�ID',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��ɫ�ֶι���'
;

CREATE TABLE `sys_roleMenuButtonMap` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RoleCode` varchar(50) NOT NULL COMMENT '��ɫ���� �����ֶι���Ψһֵ����',
  `MenuCode` varchar(50) NOT NULL COMMENT '�˵����� �����ֶι���Ψһֵ����',
  `ButtonCode` varchar(50) NOT NULL COMMENT '�¼����� �����ֶι���Ψһֵ����',
  PRIMARY KEY (`ID`,`RoleCode`,`MenuCode`,`ButtonCode`),
  UNIQUE KEY `uni_sys_roleMenuButtonMap_RoleCode_MenuCode_ButtonCode` (`RoleCode`,`MenuCode`,`ButtonCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��ɫ-�˵��¼�������'
;

CREATE TABLE `sys_roleMenuMap` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RoleCode` varchar(50) NOT NULL COMMENT '��ɫ���� �����ֶι���Ψһֵ����',
  `MenuCode` varchar(50) NOT NULL COMMENT '�˵����� �����ֶι���Ψһֵ����',
  PRIMARY KEY (`ID`,`RoleCode`,`MenuCode`),
  UNIQUE KEY `uni_sys_roleMenuMap_RoleCode_MenuCode` (`RoleCode`,`MenuCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='��ɫ-�˵�������'
;

CREATE TABLE `sys_user` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Code` varchar(50) NOT NULL COMMENT '�û����� Ψһ�� ��¼��',
  `Seq` int(4) DEFAULT '0' COMMENT '���� Ĭ��Ϊ0',
  `Name` varchar(50) DEFAULT NULL COMMENT '�û�����',
  `Description` varchar(200) DEFAULT NULL COMMENT '����',
  `Password` varchar(50) DEFAULT NULL COMMENT '����MD5',
  `RoleName` varchar(50) DEFAULT NULL COMMENT '��ɫ����',
  `OrganizeName` varchar(50) DEFAULT NULL COMMENT '��֯����',
  `ConfigJSON` varchar(200) DEFAULT NULL COMMENT '��������json��ʽ',
  `IsEnable` int(1) DEFAULT NULL COMMENT '�Ƿ���� 1�� 0��',
  `LoginCount` int(4) DEFAULT NULL COMMENT '��¼����',
  `LastLoginDate` datetime DEFAULT NULL COMMENT '����¼ʱ��',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `ModeType` int(4) DEFAULT NULL COMMENT 'ģ��id(1 ����� 2 �ֿ�� )ö������',
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ����   ��ģ��id=2 �ֿ�ε�ʱ��������\r\n�������ֲ�ͬ�ֿ���û�\r\n',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_user_UserCode` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�û���'
;
INSERT sys_user (Code,Seq,Name,Password,IsEnable,LoginCount,LastLoginDate,ModeType) VALUES ( 'admin',0,'����Ա','202cb962ac59075b964b07152d234b70',1,1,'2015/9/25 16:16:12',1);
INSERT sys_user (Code,Seq,Name,Password,IsEnable,LoginCount,LastLoginDate,CreatePerson,CreateDate,UpdatePerson,UpdateDate,ModeType) VALUES ( 'conan',0,'conan','e10adc3949ba59abbe56e057f20f883e',1,0,'0001/1/1 0:00:00','admin','2015/9/24 21:41:17','admin','2015/9/25 16:16:54',1);

CREATE TABLE `sys_userOrganizeMap` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserCode` varchar(50) NOT NULL COMMENT '�û����� �����ֶι���Ψһֵ����',
  `OrganizeCode` varchar(50) NOT NULL COMMENT '��֯���� �����ֶι���Ψһֵ����',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_userOrganizeMap_UserCode_OrganizeCode` (`UserCode`,`OrganizeCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='�û�-��֯������'
;

CREATE TABLE `sys_userRoleMap` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserCode` varchar(50) NOT NULL COMMENT '�û����� �����ֶι���Ψһֵ����',
  `RoleCode` varchar(50) NOT NULL COMMENT '��ɫ���� �����ֶι���Ψһֵ����',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_sys_userRoleMap_UserCode_RoleCode` (`UserCode`,`RoleCode`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�û�-��ɫ������'
;
INSERT sys_userRoleMap (UserCode,RoleCode) VALUES ( '1','656565');
INSERT sys_userRoleMap (UserCode,RoleCode) VALUES ( '1','676776');
INSERT sys_userRoleMap (UserCode,RoleCode) VALUES ( 'admin','343443');

CREATE TABLE `test` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `uu` int(10) DEFAULT NULL,
  `yy` tinyint(1) DEFAULT NULL,
  `ttt` decimal(10,2) DEFAULT NULL,
  `ooo` datetime DEFAULT NULL,
  `yty` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8
;
INSERT test (uu,yy) VALUES ( 2147483647,True);
INSERT test (uu,ttt) VALUES ( 0,99999999.99);
INSERT test (ttt) VALUES ( 43444444.00);
INSERT test (uu) VALUES ( 767676);

CREATE TABLE `testtable` (
  `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(500) DEFAULT NULL,
  `creTime` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=114 DEFAULT CHARSET=utf8
;
INSERT testtable (name) VALUES ( 'fdddddddddddddddddddd');
INSERT testtable (name,creTime) VALUES ( 'fdddddddddddddddddddd','2015/9/3 16:23:16');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:15');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:19');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:18');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:16');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:15');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:16');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:20');
INSERT testtable (name,creTime) VALUES ( 'fdddddddddddddddddddd','2015/9/3 16:23:16');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:15');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:23:21');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:09');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:09');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:09');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:10');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:10');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:11');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:11');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:11');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:12');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:12');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:12');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:13');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:13');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:14');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:14');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:15');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:15');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:15');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:15');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:16');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:16');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:16');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:17');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:17');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/3 16:25:17');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/6 9:35:40');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/6 9:36:40');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/6 9:37:11');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/6 9:37:15');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/6 9:37:31');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/6 9:37:36');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/6 9:47:53');
INSERT testtable (name,creTime) VALUES ( '001','2015/9/6 9:53:38');

CREATE TABLE `Tree` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `ParentID` int(10) DEFAULT NULL,
  `State` varchar(50) DEFAULT NULL,
  `Url` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8
;
INSERT Tree (Name,ParentID,State) VALUES ( '�й�',0,'open');
INSERT Tree (Name,ParentID) VALUES ( '����',1);
INSERT Tree (Name,ParentID) VALUES ( '�Ϻ�',1);
INSERT Tree (Name,ParentID) VALUES ( '����',2);
INSERT Tree (Name,ParentID) VALUES ( 'ͬ��',4);
INSERT Tree (Name,ParentID) VALUES ( '�ֶ�',3);

CREATE TABLE `warehouse` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `Code` varchar(50) NOT NULL COMMENT '�ֿ��� Ψһֵ',
  `Name` varchar(50) NOT NULL COMMENT '�ֿ�����',
  `IsEnable` int(1) NOT NULL COMMENT '�Ƿ���� 0���� 1����',
  `CreatePerson` varchar(50) NOT NULL COMMENT '�����ˣ������û�������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸��ˣ��û�������',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  `Remark` varchar(500) DEFAULT NULL COMMENT '��ע',
  `Address` varchar(100) DEFAULT NULL COMMENT '�ֿ��ַ',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouse_Code` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�ֿ���Ϣ��'
;

CREATE TABLE `warehouseAreaStruct` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `Name` varchar(50) NOT NULL COMMENT '�ṹ���ƣ����磺�����š��顢�㡢λ��ͬһ�ṹ�ڱ���Ψһ',
  `Code` varchar(50) NOT NULL COMMENT '�ṹ����(��λ�������ɲ���)',
  `ParentID` int(10) NOT NULL COMMENT '����ID',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�����ṹ��'
;

CREATE TABLE `warehouseBookingProductsSku` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ���',
  `ProductsID` int(10) DEFAULT NULL COMMENT '�ֿ����Ʒ���ʶ',
  `ProductsSkuID` int(10) DEFAULT NULL COMMENT '�ֿ��Sku���ʶ',
  `InventoryModel` int(1) DEFAULT NULL COMMENT '���ۼ�ģʽ 0���ۼ� 1�����ۼ�',
  `BookingNum` int(4) DEFAULT NULL COMMENT 'Ԥ������',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouseBookingProdectsSku_WarehouseCode_ProductsSkuID` (`WarehouseCode`,`ProductsSkuID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `warehouseExpress` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `Name` varchar(50) NOT NULL COMMENT '�������',
  `IsCod` int(1) NOT NULL COMMENT '�Ƿ�������� 0���� 1����',
  `IsZt` int(1) NOT NULL COMMENT '�Ƿ����� 0���� 1����',
  `ContactPerson` varchar(50) DEFAULT NULL COMMENT '��ϵ��',
  `ContactTel` varchar(50) DEFAULT NULL COMMENT '��ϵ�绰',
  `IsSetTypeArea` int(1) NOT NULL COMMENT '�Ƿ��������� 0�� 1�ǣ����ѡ���ǣ����������򣬽�����ѡ��ÿ�ݣ�',
  `LogisticsID` int(10) DEFAULT NULL COMMENT '�������ʶ',
  `PrinterType` int(4) NOT NULL COMMENT '��ӡ���ͣ���ʽ����������ѡ�� ö�����͡���ʽ=1 ����=2',
  `PrinterName` varchar(50) DEFAULT NULL COMMENT 'Ĭ�ϴ�ӡ������',
  `Width` decimal(18,3) DEFAULT NULL COMMENT 'ֽ�ſ�� mm',
  `Height` decimal(18,3) DEFAULT NULL COMMENT 'ֽ�Ÿ߶� mm',
  `TemplateName` varchar(50) DEFAULT NULL COMMENT 'ģ������',
  `HotType` int(1) DEFAULT NULL COMMENT '�������� 1��ʹ�ÿ������Ľӿڡ�����Ҫ���ݹ�˾����ͻ���š��������Ϣ��2��ʹ���Ա������񣩵����浥�ӿ�',
  `CustId` varchar(50) DEFAULT NULL COMMENT '�ͻ����',
  `CustKey` varchar(100) DEFAULT NULL COMMENT '�ͻ�����',
  `InterFaceUrl` varchar(100) DEFAULT NULL COMMENT '�ӿڵ�ַ',
  `BillingMethods` int(1) NOT NULL COMMENT '�Ʒѷ�ʽ 0�����ؼƷ� 1�������Ʒ�',
  `Seq` int(4) NOT NULL COMMENT '����',
  `IsEnable` int(1) NOT NULL COMMENT '�Ƿ���� 0���� 1����',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�ֿ��ݱ�'
;

CREATE TABLE `warehouseInventoryWarn` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `WarehouseCode` varchar(50) DEFAULT NULL COMMENT '�ֿ���',
  `WarnType` int(1) DEFAULT NULL COMMENT 'Ԥ������ 0������ 1��������Ʒ 2������SKU',
  `ProductsID` int(10) DEFAULT NULL COMMENT '�ֿ����Ʒ���ʶ',
  `ProductsSkuID` int(10) DEFAULT NULL COMMENT '�ֿ��Sku���ʶ',
  `ProductsWarn` int(4) DEFAULT NULL COMMENT '��Ʒ���Ԥ������',
  `ProductsSkuWarn` int(4) DEFAULT NULL COMMENT '��ƷSKU���Ԥ������',
  `CreatePerson` varchar(50) DEFAULT NULL COMMENT '������',
  `CreateDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
;

CREATE TABLE `warehouseLocation` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `StructName` varchar(50) NOT NULL COMMENT '�ṹ���ƣ����磺����λ��',
  `StructCode` varchar(50) NOT NULL COMMENT '�ṹ����(��λ�������ɲ���)',
  `Code` varchar(50) NOT NULL COMMENT '��λ����(�ɸ�����ṹ����+����ṹ�������� )',
  `Name` varchar(50) DEFAULT NULL COMMENT '��λ����',
  `TypeID` int(1) NOT NULL COMMENT '��������id��������ת��=1����Ʒ��=2��������=3��������=4 ��ѡ��',
  `ParentID` int(10) NOT NULL COMMENT '����ID',
  `IsEnable` int(1) NOT NULL COMMENT '�Ƿ���ã����ǣ���',
  `Seq` int(10) NOT NULL COMMENT '����ID',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouseLocation_WarehouseCode_Code` (`WarehouseCode`,`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�ֿ��λ��'
;

CREATE TABLE `warehouseLocationProducts` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `LocationID` int(10) NOT NULL COMMENT '��λID(��λ��Ϣ������)',
  `LocationTypeID` int(1) NOT NULL COMMENT '��λ����ID��������ת��=1����Ʒ��=2��������=3��������=4 ��ѡ��',
  `ProductsID` int(10) NOT NULL COMMENT '��Ʒ���ʶ',
  `ProductsSkuID` int(10) NOT NULL COMMENT '��ƷSku���ʶ',
  `ProductsBatchID` int(10) NOT NULL COMMENT '��Ʒ���α��ʶ',
  `ProductsBatchCode` varchar(50) NOT NULL COMMENT '��Ʒ���κ�',
  `ProductionDate` datetime NOT NULL COMMENT '��������',
  `ShelfLife` int(10) NOT NULL COMMENT '�����ڣ��죩',
  `KyNum` int(10) NOT NULL COMMENT '��������',
  `ZyNum` int(10) NOT NULL COMMENT 'ռ������',
  `DjNum` int(10) NOT NULL COMMENT '��������',
  `SdNum` int(10) NOT NULL COMMENT '�ֶ�����',
  `ZkNum` int(10) NOT NULL COMMENT '�ڿ�����',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouseLocationProducts_WLPP` (`WarehouseCode`,`LocationID`,`ProductsSkuID`,`ProductsBatchID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�ֿ��λ����Ʒ��Ϣ��'
;

CREATE TABLE `warehouseOutbound` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `BillNo` varchar(50) NOT NULL COMMENT '���ⵥ��� ϵͳ����Ψһ',
  `BillType` int(4) NOT NULL COMMENT '�������� �ɹ����10,�ɹ��˻�20,�������30,��������40,�˻����50,���۳���60,�̵�70,��λ80,�������90,��������100',
  `ParentBillNo` varchar(50) DEFAULT NULL COMMENT '�������ⵥ��',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `ErpOrderCode` varchar(100) NOT NULL COMMENT 'ϵͳ�����š�ERP�Զ����ɵĶ����ţ������ظ�������Ϊ�ա�����Ψһ��ʶ',
  `OutOrderCode` varchar(1000) DEFAULT NULL COMMENT '�ⲿ�����š�ϵͳ����ʱ�Ķ����ţ�ERP�ڲ��ֶ����ʱ���û��������Ϊ��',
  `ShopID` int(10) NOT NULL COMMENT '���̱��ʶ',
  `OrderSource` int(4) DEFAULT NULL COMMENT '������Դ ö��',
  `OrderType` int(1) NOT NULL COMMENT '��������0���Է���1������',
  `BuyNickName` varchar(50) NOT NULL COMMENT '����ǳ�',
  `BuyName` varchar(50) NOT NULL COMMENT '�������',
  `BuyTel` varchar(50) NOT NULL COMMENT '��ҵ绰',
  `BuyAddr` varchar(100) NOT NULL COMMENT '��ҵ�ַ',
  `BuyPostCode` varchar(50) NOT NULL COMMENT '����ʱ�',
  `BuyMessage` varchar(200) DEFAULT NULL COMMENT '�������',
  `SellerRemark` varchar(500) DEFAULT NULL COMMENT '���ұ�ע',
  `Status` int(4) NOT NULL COMMENT '���ⵥ״̬(0�����ɡ�10�ȴ����⡢20�ѳ��⡢99��ȡ��)',
  `GroupID` int(10) NOT NULL COMMENT '��ӡ����ID',
  `LogisticsID` int(10) NOT NULL COMMENT '������˾ID',
  `ExpressID` int(10) NOT NULL COMMENT '�µ�ѡ���ݹ�˾ID',
  `DeliveryExpressID` int(10) NOT NULL COMMENT '������ݹ�˾ID',
  `WaybillNo` varchar(50) DEFAULT NULL COMMENT '�˵���',
  `ExpressPrintDate` datetime DEFAULT NULL COMMENT '��ݴ�ӡʱ��',
  `PayDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `PaymentMethod` int(1) NOT NULL COMMENT '���ʽ 0:����֧�� 1����������',
  `BuyCodFee` decimal(18,3) NOT NULL COMMENT '��һ�����������',
  `TradingNumber` varchar(100) DEFAULT NULL COMMENT '���׺�',
  `PaymentAccount` varchar(100) DEFAULT NULL COMMENT '�����˺�',
  `IsPreSale` int(1) NOT NULL COMMENT '�Ƿ�Ԥ�۳��ⵥ 0�� 1��',
  `IsHang` int(1) DEFAULT NULL COMMENT '�Ƿ���� 0�� 1��',
  `IsScanCheck` int(1) NOT NULL COMMENT '�Ƿ�У�� 0�� 1��',
  `ScanCheckDate` datetime DEFAULT NULL COMMENT 'У��ʱ��',
  `TotalWeight` decimal(18,3) NOT NULL COMMENT 'ʵ�ʰ�������',
  `TotalAmount` decimal(18,3) NOT NULL COMMENT '���ⵥ�ܽ��',
  `CancelRemark` varchar(1000) DEFAULT NULL COMMENT 'ȡ����ע',
  `CancelDate` datetime DEFAULT NULL COMMENT 'ȡ��ʱ��',
  `ExpectedDeliDate` datetime DEFAULT NULL COMMENT '��������ʱ��',
  `ShopFreight` decimal(18,3) NOT NULL COMMENT 'ʵ���˷ѣ������չ˿ͣ�',
  `ExpressFreight` decimal(18,3) NOT NULL COMMENT 'Ӧ���˷�(�ֿ⸶����ݹ�˾)',
  `IsNeedInvoice` int(1) NOT NULL COMMENT '�Ƿ���Ҫ��Ʊ',
  `DeliveryDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouseOutbound_BillNo` (`BillNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='���۳��ⵥ��'
;

CREATE TABLE `warehouseOutboundItem` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `OutboundID` int(10) NOT NULL COMMENT '���ⵥ���ʶ',
  `OutboundBillNo` varchar(50) NOT NULL COMMENT '���ⵥ���',
  `ErpOrderCode` varchar(100) NOT NULL COMMENT 'ϵͳ������',
  `OrdItemID` int(10) NOT NULL COMMENT '������ϸ���ʶ',
  `Ord_OuterItemID` int(10) NOT NULL COMMENT '�ⲿ������ϸ���ʶ',
  `BillType` int(4) NOT NULL COMMENT '�������Ͳɹ����10,�ɹ��˻�20,�������30,��������40,�˻����50,���۳���60,�̵�70,��λ80,�������90,��������100',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `BrandID` int(10) NOT NULL COMMENT 'Ʒ�Ʊ��ʶ',
  `BrandName` varchar(50) DEFAULT NULL COMMENT 'Ʒ������',
  `CategoryID` int(10) NOT NULL COMMENT '������ʶ',
  `CategoryName` varchar(50) DEFAULT NULL COMMENT '��������',
  `ProductsID` int(10) NOT NULL COMMENT '��Ʒ���ʶ',
  `ProductsCode` varchar(50) NOT NULL COMMENT '��Ʒ����',
  `ProductsName` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsNo` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsWeight` decimal(18,3) NOT NULL COMMENT '��Ʒ����',
  `ProductsSkuID` int(10) NOT NULL COMMENT '��ƷSku���ʶ',
  `ProductsSkuCode` varchar(50) NOT NULL COMMENT 'Sku��',
  `ProductsSkuSaleprop` varchar(100) DEFAULT NULL COMMENT '��������(��ɫ����ɫ ���S)',
  `ProductsNum` int(10) NOT NULL COMMENT '��Ʒ����',
  `LocationID` int(10) NOT NULL COMMENT '��λ���ʶ',
  `ProductsBatchID` int(10) NOT NULL COMMENT '��Ʒ���α��ʶ',
  `ProductsBatchCode` varchar(50) NOT NULL COMMENT '��Ʒ���κ�',
  `ProductionDate` datetime DEFAULT NULL COMMENT '��������',
  `SellingPrice` decimal(18,3) NOT NULL COMMENT '���ۼ�',
  `CostPrice` decimal(18,3) NOT NULL COMMENT '�ɱ��� ',
  `DeliveryDate` datetime DEFAULT NULL COMMENT '����ʱ��',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='���۳��ⵥ��ϸ��'
;

CREATE TABLE `warehouseOutInStock` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `BillNo` varchar(50) NOT NULL COMMENT '����ⵥ�ݱ�� ϵͳ����Ψһ',
  `BillType` int(4) NOT NULL COMMENT '�������� ö�� �ɹ����10,�ɹ��˻�20,�������30,��������40,�˻����50,���۳���60,�̵�70,��λ80,�������90,��������100',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `Status` int(4) NOT NULL COMMENT '����״̬ ö�� 0������ 10 ��ȷ��',
  `IsAuditPrice` int(1) NOT NULL COMMENT '�Ƿ���� 0 �� 1 ��',
  `SourceID` int(10) NOT NULL COMMENT '��Դ���ݱ�ʶ',
  `SourceNo` varchar(50) DEFAULT NULL COMMENT '��Դ���ݱ��',
  `SuppID` int(10) NOT NULL COMMENT '��Ӧ�̱��ʶ',
  `MainName` varchar(50) NOT NULL COMMENT '������',
  `CountName` varchar(50) NOT NULL COMMENT '�����',
  `ExpressID` int(10) NOT NULL COMMENT '��ݹ�˾ID',
  `Remark` varchar(500) DEFAULT NULL COMMENT '��ע',
  `OutInDate` datetime NOT NULL COMMENT '��������',
  `ConfirmDate` datetime DEFAULT NULL COMMENT 'ȷ��ʱ��',
  `IsDxYs` int(1) NOT NULL COMMENT '����Ƿ����Ԥ�� 0�� 1��',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouseOutInStock_BillNo` (`BillNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='����ⵥ��'
;

CREATE TABLE `warehouseOutInStockItem` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `OutInStockID` int(10) NOT NULL COMMENT '����ⵥ���ʶ',
  `OutInStockBillNo` varchar(50) NOT NULL COMMENT '����ⵥ�ݱ�� ϵͳ����Ψһ',
  `BillType` int(4) NOT NULL COMMENT '�������� ö�� �ɹ����10,�ɹ��˻�20,�������30,��������40,�˻����50,���۳���60,�̵�70,��λ80,�������90,��������100',
  `SourceID` int(10) NOT NULL COMMENT '��Դ���ݱ�ʶ',
  `SourceNo` varchar(50) DEFAULT NULL COMMENT '��Դ���ݱ��',
  `StockWay` int(1) NOT NULL COMMENT '����ⷽ�� -1���� 1���',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `Status` int(4) NOT NULL COMMENT '������ϸ״̬ö��0���� 10 ��ȷ��',
  `IsAuditPrice` int(1) NOT NULL COMMENT '�Ƿ����0��1��',
  `ProductsID` int(10) NOT NULL COMMENT '��Ʒ���ʶ',
  `ProductsCode` varchar(50) NOT NULL COMMENT '��Ʒ����',
  `ProductsName` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsNo` varchar(50) DEFAULT NULL COMMENT '��Ʒ����',
  `ProductsSkuID` int(10) NOT NULL COMMENT '��ƷSku���ʶ',
  `ProductsSkuCode` varchar(50) NOT NULL COMMENT 'Sku��',
  `ProductsSkuSaleprop` varchar(100) DEFAULT NULL COMMENT '��������(��ɫ����ɫ ���S)',
  `ProductsNum` int(10) NOT NULL COMMENT '��Ʒ����',
  `LocationID` int(10) NOT NULL COMMENT '��λ���ʶ',
  `ProductsBatchID` int(10) NOT NULL COMMENT '��Ʒ���α��ʶ',
  `ProductsBatchCode` varchar(50) NOT NULL COMMENT '��Ʒ���κ�',
  `ProductionDate` datetime DEFAULT NULL COMMENT '��������',
  `CostPrice` decimal(18,3) NOT NULL COMMENT '�ɱ��� ',
  `Remark` varchar(500) DEFAULT NULL COMMENT '��ע',
  `ConfirmDate` datetime DEFAULT NULL COMMENT 'ȷ��ʱ��',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='����ⵥ��ϸ��'
;

CREATE TABLE `warehouseOutInStockLog` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `BillType` int(4) NOT NULL COMMENT '�������� �ɹ����10,�ɹ��˻�20,�������30,��������40,�˻����50,���۳���60,�̵�70,��λ80,�������90,��������100',
  `SourceID` int(10) NOT NULL COMMENT '��Դ���ݱ�ʶ',
  `SourceNo` varchar(50) NOT NULL COMMENT '��Դ���ݱ��',
  `SourceItemID` int(10) NOT NULL COMMENT '��Դ������ϸ��ʶ',
  `StockWay` int(1) NOT NULL COMMENT '����ⷽ�� 1��� -1����',
  `ProductsID` int(10) NOT NULL COMMENT '��Ʒ���ʶ',
  `ProductsCode` varchar(50) NOT NULL COMMENT '��Ʒ����',
  `ProductsSkuID` int(10) NOT NULL COMMENT '��ƷSku���ʶ',
  `ProductsSkuCode` varchar(50) NOT NULL COMMENT '��ƷSku����',
  `LocationID` int(10) NOT NULL COMMENT '��λID',
  `ProductsBatchID` int(10) NOT NULL COMMENT '��Ʒ���α��ʶ',
  `ProductsBatchCode` varchar(50) NOT NULL COMMENT '��Ʒ���κ�',
  `ProductionDate` datetime NOT NULL COMMENT '��������',
  `ShelfLife` int(10) NOT NULL COMMENT '������(��)',
  `Num` int(10) NOT NULL COMMENT '���������',
  `CostPrice` decimal(18,3) NOT NULL COMMENT '�ɱ���',
  `Remark` varchar(500) DEFAULT NULL COMMENT '��ע',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�������־��'
;

CREATE TABLE `warehouseProducts` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `ProductsID` int(10) NOT NULL COMMENT '�������Ʒ���ʶ',
  `ProductsStatus` int(1) DEFAULT NULL COMMENT '��Ʒ����״̬ ������=1 �ֿ���=2 ',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouseProducts_WarehouseCode_ProductsID` (`WarehouseCode`,`ProductsID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�ֿ���Ʒ��'
;

CREATE TABLE `warehouseProductsBatch` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `ProductsID` int(10) NOT NULL COMMENT '�������Ʒ���ʶ',
  `ProductsSkuID` int(10) NOT NULL COMMENT '�������ƷSku���ʶ',
  `BatchCode` varchar(50) NOT NULL COMMENT '���κ�',
  `ProductionDate` datetime NOT NULL COMMENT '��������',
  `ShelfLife` int(10) NOT NULL COMMENT '������(��)',
  `CostPrice` decimal(18,3) NOT NULL COMMENT '�ɱ���',
  `KyNum` int(10) NOT NULL COMMENT '��������',
  `ZyNum` int(10) NOT NULL COMMENT 'ռ������',
  `DjNum` int(10) NOT NULL COMMENT '��������',
  `SdNum` int(10) NOT NULL COMMENT '�ֶ���������',
  `ZkNum` int(10) NOT NULL COMMENT '�ڿ�����',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouseProductsBatch_WarehouseCodeProductsSkuIDBatchCode` (`WarehouseCode`,`ProductsSkuID`,`BatchCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�ֿ���Ʒ���α�'
;

CREATE TABLE `warehouseProductsSku` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '����ID',
  `WarehouseCode` varchar(50) NOT NULL COMMENT '�ֿ���',
  `ProductsID` int(10) NOT NULL COMMENT '�������Ʒ���ʶ',
  `ProductsSkuID` int(10) NOT NULL COMMENT '�����Sku���ʶ',
  `CreatePerson` varchar(50) NOT NULL COMMENT '������',
  `CreateDate` datetime NOT NULL COMMENT '����ʱ��',
  `UpdatePerson` varchar(50) DEFAULT NULL COMMENT '�޸���',
  `UpdateDate` datetime DEFAULT NULL COMMENT '�޸�ʱ��',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uni_warehouseProductsSku_WarehouseCode_ProductsSkuID` (`WarehouseCode`,`ProductsSkuID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC COMMENT='�ֿ���ƷSku��'
;
