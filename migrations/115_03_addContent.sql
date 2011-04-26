Insert Into Analit.MenuField (Name, Link)
values ('Программа' , 'Content/Программа'),
('Пользователям','https://stat.analit.net/ci/auth/logon.aspx'),
('Сертификат','Content/Сертификат'),
('Поиск лекарств','http://www.analit.net/apteka');

Insert into Analit.IVRNContent (Content, ViewName)
values ('<center><strong>Выберите файлы для загрузки:</strong></center>
<br/>
<table class=\"table\" style=\"width:100%;\">
 <tbody>
 <thead>
  <tr class=\"caption\"> 
    <td>Текущая версия программы</td>
    <td>Комментарий</td>
  </tr>
 </thead>
  <tr> 
    <td><a href=\"AF1385.exe\">Загрузить</a>(рекомендуется)</td>
    <td> Загрузка по HTTP </td>
  </tr>

  <tr > 
    <td> <a href=\"ftp://ftp.analit.net/anonymous/AF1385.exe\">Загрузить</a>(альтернативно)</td>
    <td>Загрузка по FTP</td>
  </tr>
 </tbody>
</table>
</center>','Программа'),
('<center><strong>Загрузка программы для старых версий ОС Microsoft Windows (98, Millennium) и эмуляторов Windows на не-Windows платформах:</strong>
<br/>
<table class=\"table\" style=\"width:100%;\">
 <tbody>
 <thead>
  <tr class=\"caption\"> 
    <td>Текущая версия программы</td>
    <td>Комментарий</td>
  </tr>
 </thead>
  <tr> 
    <td><a href=\"http://www.analit.net/ftp/AF705.exe\">Загрузить</a>(рекомендуется)</td>
    <td> Загрузка по HTTP </td>
  </tr>

  <tr > 
    <td> <a href=\"ftp://ftp.analit.net/anonymous/AF705.exe\">Загрузить</a>(альтернативно)</td>
    <td>Загрузка по FTP</td>
  </tr>
 </tbody>
</table>
</center>','Для старых версий ОС'),
('<table width="100%" cellpadding="0" cellspacing="0" border="0" class="table">

<form name=form1>

  <tr>

    <td width=\"100%\" class=\"value\">

<h2>Установка сертификата SSL</h2>

<a href=\"http://cert.analit.net/CertEnroll/acdcserv.adc.analit.net_!0421!043b!0443!0436!0431!0430%20!0441!0435!0440!0442!0438!0444!0438!043a!0430!0446!0438!0438%20!0410!041a%20!0022!0418!043d!0444!043e!0440!0443!043c!0022(1).crt\"><h3>Загрузить сертификат</h3></a>

<font face=\"Verdana, Arial, Helvetica, sans-serif\">

<p>Для работы с нашим сайтом в защищенном режиме вам потребуются принять наш сертификат. Он используются для цифровой подписи S/MIME-сообщений и для работы с сервером по SSL (https), а также для проверки подписи сертификатов, которые будут выданы на этом сайте.</p>

<p>Для установки сертификата SSL нужно:</p>

<p>Открыв <a href=\"http://cert.analit.net/CertEnroll/acdcserv.adc.analit.net_!0421!043b!0443!0436!0431!0430%20!0441!0435!0440!0442!0438!0444!0438!043a!0430!0446!0438!0438%20!0410!041a%20!0022!0418!043d!0444!043e!0440!0443!043c!0022(1).crt\">файл</a>, который содержит наш сертификат SSL, прямо со страницы либо после предварительного сохранения на диск. После открытия файла должно появиться следующее окно, в котором необходимо нажать на кнопку &#147;Установить Сертификат...&#148; (&#147;Install Certificate...&#148;).</p>

<div align=\"center\"><img src=\"crt1.gif\" align=\"center\"></div>

<p>Запустится мастер установки сертификатов, в первом окне которого необходимо нажать кнопку \"Далее >\" (\"Next >\").</p>

<div align=\"center\"><img src=\"crt2.gif\" align=\"center\"></div>

<p>Cледующий шаг - необходимо выбрать опцию \"Поместить все сертификаты в следующее хранилище\" (\"Place all certificates in the following store\") и нажать на кнопку \"Обзор...\" (\"Browse...\").</p>

<div align=\"center\"><img src=\"crt3.gif\" align=\"center\"></div>

<p>В открывшемся окне необходимо выбрать \"Доверенные корневые центры сертификации\" (\"Trusted Root Certification Authorities\") и нажать кнопку \"OK\".</p>

<div align=\"center\"><img src=\"crt4.gif\" align=\"center\"></div>

<p>Для продолжения установки нажмите \"Далее >\" (\"Next >\").</p>

<div align=\"center\"><img src=\"crt5.gif\" align=\"center\"></div>

<p>Далее проверьте информацию и нажмите \"Готово\" (\"Finish\") для установки.</p>

<div align=\"center\"><img src=\"crt6.gif\" align=\"center\"></div>

<p>В процессе установки будет задан вопрос - действительно ли вы хотите установить данный сертификат. На него необходимо ответить нажатием кнопки \"Да\" (\"Yes\").</p>

<p>В подтверждение успешной установки вы должны получить следующее сообщение:</p>

<div align=\"center\"><img src=\"crt8.gif\" align=\"center\"></div>

</font>

</form>

</table>
','Сертификат');