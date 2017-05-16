搞不懂为啥？

 var projectRulesJson = '
[{"RuleItemId":"1c6bce3575c84bd08fe9d8c9e2b2262a","SortNo":1,"RuleItemText":"1.资产方：上海厚本金融信息服务有限公司\n2.增信方：中华联合财险保险股份有限公司上海分公司\n3.超级放款人：欧阳君、冯巍","RuleItemStatus":10,"Remark":"","ProjectId":"54b074e7-3d31-461e-889d-52d75af2a883"},{"RuleItemId":"29e75bdf54814696a1e38204e12b227f","SortNo":2,"RuleItemText":"审核资料是否清晰完整：\n一、借款协议\t\n二、债权转让及远期回购协议\t\n三、产品说明书\t\n四、欧阳君、冯巍身份证(首次交易提供)\t\n五、履约保单\t\n六、欧阳君银行账户信息（首次交易提供）\t\n七、底层放款凭证","RuleItemStatus":0,"Remark":"","ProjectId":"54b074e7-3d31-461e-889d-52d75af2a883"},{"RuleItemId":"7b7350a2e20b4551ae624ebabfc137b7","SortNo":3,"RuleItemText":"审核要素：是否有清晰的盖章签字按手印、签署日期、骑缝章等：\n一、借款协议及附件：\n①借款人：与保单投保人信息一致；②出借人：欧阳君（信息与身份证一致）；③借款金额：与保单贷款金额信息一致，不超过20万；④期限：起息日不晚于产品说明书起息日，到期日不早于产品说明书到期日；⑤利率：年化不高于36%；⑥借款协议签署日期：不晚于债转协议签署日；","RuleItemStatus":0,"Remark":"","ProjectId":"54b074e7-3d31-461e-889d-52d75af2a883"},{"RuleItemId":"d0f68ab1a0a9476e98f3a94c86778d33","SortNo":4,"RuleItemText":"二、债权转让协议：\n①转让人：欧阳君（信息与身份证一致）\n②受让人：冯巍（信息与身份证一致）\n③转让价款：小或等于“借款协议借款金额之和”小或等于“保单贷款金额之和”等于“产品说明书本次金额规模”；","RuleItemStatus":0,"Remark":"","ProjectId":"54b074e7-3d31-461e-889d-52d75af2a883"},{"RuleItemId":"33e91fa442b84914acb681e2005e954c","SortNo":5,"RuleItemText":"三、远期回购确认函：\n①转让人、受让人、债转协议编号与债权转让协议一致，利率和期限与项目启动表一致，利息计算公式等要素与产品说明书一致","RuleItemStatus":0,"Remark":"","ProjectId":"54b074e7-3d31-461e-889d-52d75af2a883"},{"RuleItemId":"2ce4876b0fd84999a0f0a21a39b6ebbf","SortNo":6,"RuleItemText":"四、产品说明书：\n依据债权转让协议及远期回购确认函的信息核对，要素与债权转让协议一致：\n①发行人：上海厚本金融信息服务有限公司；\n②本次产品规模：小或等于“借款协议借款金额之和”小或等于“保单贷款金额之和”；\n③期限：起息日与我司拟放款日期一致，起息日、到期日在保单期限内\n④利率：与远期回购确认函一致\n⑤债权转让协议编号：与债转协议编号一致","RuleItemStatus":0,"Remark":"","ProjectId":"54b074e7-3d31-461e-889d-52d75af2a883"},{"RuleItemId":"ec5b0183248d470081bc92025f349160","SortNo":7,"RuleItemText":"五、保单：\n①投保人：借款协议借款人\n②被保险人：冯巍\n③贷款金额：大于或等于债转协议金额\n④保险金额：汇总金额不小于产品说明书的本次金额规模+预期收益之和\n⑤保险期间：生效日不晚于债转协议起息日，保单到期日不早于债转协议到期日\n⑥内容编号：与债转协议、借款协议编号一致\n⑦保险名称：A借款人履约保证保险保险单B《借款人履约保证保险》C《中华联合财产保险股份有限公司借款人履约保证保险条款》\n⑧盖章：中华联合财产保险股份有限公司上海分公司\n⑨真伪检查：保险公司官网可查","RuleItemStatus":0,"Remark":"","ProjectId":"54b074e7-3d31-461e-889d-52d75af2a883"},{"RuleItemId":"469e270cc4a340aa9e41ddbc07db45bb","SortNo":8,"RuleItemText":"六、底层放款凭证：\n① 放款人名字为上海厚本金融信息服务有限公司或欧阳君，收款人名字为实际借款人，转账实际在借款协议日期之后。","RuleItemStatus":0,"Remark":"","ProjectId":"54b074e7-3d31-461e-889d-52d75af2a883"}]
 ';
 
 var projectRules = JSON.parse(projectRulesJson);



    <%--    var projectRulesJson = '<%= Model.Rules.ToJson()%>';

        var projectRules = JSON.parse(projectRulesJson);--%>

    
        var projectRules = <%= Model.Rules.ToJson()%>;
