<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdDocumentUpload.ascx.cs" Inherits="Form_SOAP_Control_Template_Specialty_StdDocumentUpload" %>


<div class="col-lg-12" style="margin: 0px; padding-right: 0px">
     <div class="modal-dialog mini-dialog" style="margin-top: -15px;">
         <div class="modal-header mini-header">
             <label>Upload Document</label>   
        </div>
         <div class="modal-body" style="padding-top: 0px; padding-bottom: 25px" id="modal_upload_document" runat="server">
             <asp:UpdatePanel runat="server" ID="upUploadDocument">
                <ContentTemplate>

                    <div class="row border-bottom" style="padding-top:10px; padding-left:20px;">
                        <div class="col-2">                
                            <a class="btn btn-default btn-sm" style="color:white; background-color:#1a2269" href="javascript:ShowDrawImage();" id="btnShowAttachment-O">
                                    Add Picture And Document
                            </a>
                        </div>
                    </div>
                    <asp:HiddenField ID="jsonDataHolder" ClientIDMode="Static" runat="server"/>
                     <div class="row border-bottom" style="padding-top:10px;">
                        <div class="col-12">
                            <ul class="list-draw-image" style="list-style-type: none;">
                    
                            </ul>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
         </div>
     </div>
</div>


<div class="modal fade" id="modal-draw-image">
	<div class="modal-dialog modal-lg">
        <div class="modal-content">
			<div class="modal-header" hidden>
				<h4 class="modal-title"><asp:Label runat="server" Font-Bold="true" ID="modalDrawImageCaption" ClientIDMode="Static" Text="Add Picture or Document"></asp:Label></h4>
            </div>
		
			<div class="modal-body">
				<div class="row" id="btnUploadSelect">
					<button type="button" onclick="btnUploadComputer()" style="margin-top:30px; width: 300px;color:white; background-color:#000080;border-radius: 4px;border-color: #000080;display:block; margin-left: auto; margin-right: auto;">Upload From Computer</button>
                    <button type="button" onclick="useTemplate('coronary')"style="width: 300px; margin-top:10px; color:white; background-color:#32cd32;border-radius: 4px;border-color: #32cd32;display:block; margin-left: auto; margin-right: auto;">Use Template</button>
				</div>
				<div class="row" id="uploadDiv">
					<div style="margin-left: auto; margin-right: auto;">
						<div id="canvastool">
							
                            <div class="form-group" id="categorytab">
								<ul class="nav nav-tabs" id="template-tab" role="templateimagelist">
									<li class="nav-item">
										<a class="nav-link active" id="coronary-tab" data-toggle="pill" href="#" role="tab" 
										    aria-selected="true">Coronary Angiography</a>
									</li>
									<li class="nav-item">
										<a class="nav-link" id="lvgraphy-tab" data-toggle="pill" href="#" role="tab" 
										    aria-selected="true">LV Graphy</a>
									</li>
								</ul>
							</div>
							<canvas id="can"></canvas>
							<img id="imglogo" src="../../../../Images/Icon/pdficon.png" style="display:none;"/>
							<br /><br />
							<label id="pdfFile" hidden></label>
							<label id="fileType" hidden></label>
							<div class="form-group" hidden>
								<label style="font-size: 18px; font-weight: bold">Notes :  </label>
								<input ID="modal-draw-image-notes" Style="margin: 0px; overflow: hidden; min-height: 35px;" placeholder="catatan..." class="text-multiline-dialog"/>
							</div>
						</div>
						<div class="drag-area" id="dragarea">
							<div class="icon"><i class="fas fa-cloud-upload-alt"></i></div>
								<header>Drag & Drop to Upload File</header>
								<span>OR</span>
								<input type="file" id="btnBrowse" >
						</div>
					</div>
			
				</div>
                <div class="row">
                    <div class="form-group" id="categorydll" style="margin-left:20px;">
						<label style="font-size: 18px; font-weight: bold">Type: </label>
						<select class="select2 form-control" id="ModalDrawImageType" runat="server" placeholder="Select.." style="outline:none !important; width:220px;display: inline;"></select>
					</div>
                    <div id="canvasEditor">
						<div class="form-group" style="margin-left:20px; width:800px; margin-top: 5px;">
							<label style="font-size: 18px; font-weight: bold">Add Text :  </label>
							<input ID="modal-draw-image-textadd" Style="margin: 0px;" />
							<button onclick="javascript:cUndo();return false;">Undo</button>
							<button onclick="javascript:cRedo();return false;">Redo</button>
						</div>
						<div>
							Line width : <select id="selWidth" style="width:50px;">
								<option value="1">1</option>
								<option value="3">3</option>
								<option value="5" selected="selected">5</option>
								<option value="7">7</option>
								<option value="9">9</option>
								<option value="11">11</option>
							</select>
							Color : <select id="selColor" style="width:100px;">
								<option value="black">black</option>
								<option value="blue" selected="selected">blue</option>
								<option value="red">red</option>
								<option value="green">green</option>
								<option value="yellow">yellow</option>
								<option value="gray">gray</option>
								<option value="white">white</option>
							</select>
							<button onclick="javascript:clearArea();return false;">Clear Area</button>
						</div>
					</div>
                </div>
			</div>
			<div class="modal-footer justify-content-right">
				<button type="button" style="color:000080; background-color:#white;border-radius: 4px;border-color: #000080;" data-dismiss="modal">Cancel</button>
				<button type="button" id="btnSubmitImg" style="color:white; background-color:#000080;border-radius: 4px;border-color: #000080;">Submit</button>
			</div>
		</div>
	</div>
</div>


  <script>
      refreshImageObjective();

      function ShowDrawImage() {
          item_imageO = [];
          OpenDrawImage();
          return true;
      }

      function HideDrawImage() {
          $('#modal-draw-image').modal('hide');
          return true;
      }

      var item_imageO = []

      function OpenDrawImage() {
          if (item_imageO.length === 0) {
              document.getElementById("btnUploadSelect").style.display = "block";
              document.getElementById("uploadDiv").style.display = "none";
              document.getElementById("btnBrowse").value = "";
              document.getElementById("canvastool").style.display = "none";
              document.getElementById("canvasEditor").style.display = "none";
              document.getElementById("categorydll").style.display = "none";
              document.getElementById("modal-draw-image-notes").value = "";
              document.getElementById("MainContent_DocumentUpload_ModalDrawImageType").value = "";

              document.getElementById("can").style.display = "block";
              document.getElementById("imglogo").style.display = "none";

              document.getElementById("canvastool").style.width = null;
              document.getElementById("canvastool").style.marginLeft = null;
          }
          else {
              let ity = item_imageO[0].image_format;
              if (ity === 'JPG') {
                  document.getElementById("btnUploadSelect").style.display = "none";
                  document.getElementById("uploadDiv").style.display = "block";
                  document.getElementById("btnBrowse").value = "";
                  document.getElementById("canvastool").style.display = "block";
                  document.getElementById("modal-draw-image-notes").value = item_imageO[0].image_remark;
                  document.getElementById("MainContent_DocumentUpload_ModalDrawImageType").value = item_imageO[0].image_type_value;
                  document.getElementById("dragarea").style.display = "none";
                  document.getElementById("canvasEditor").style.display = "block";
                  document.getElementById("can").style.display = "block";
                  document.getElementById("imglogo").style.display = "none";
                  document.getElementById("categorydll").style.display = "block";
                  document.getElementById("categorytab").style.display = "none";


                  var canvas = document.getElementById('can');
                  var img = new Image();
                  img.src = item_imageO[0].image_url;
                  canvas.width = img.width;
                  canvas.height = img.height;
                  img.onload = function () {
                      ctx.drawImage(img, 0, 0, img.width, img.height,     // source rectangle
                          0, 0, canvas.width, canvas.height);
                  }

                  document.getElementById("canvastool").style.width = img.width.toString() + "px";
                  document.getElementById("canvastool").style.marginLeft = (600 - (img.width / 2)).toString() + "px";

              }
              else {
                  document.getElementById('pdfFile').innerHTML = item_imageO[0].image_url;


                  document.getElementById("btnUploadSelect").style.display = "none";
                  document.getElementById("uploadDiv").style.display = "block";
                  document.getElementById("btnBrowse").value = "";
                  document.getElementById("canvastool").style.display = "block";
                  document.getElementById("modal-draw-image-notes").value = item_imageO[0].image_remark;
                  document.getElementById("MainContent_DocumentUpload_ModalDrawImageType").value = item_imageO[0].image_type_value;
                  document.getElementById("dragarea").style.display = "none";
                  document.getElementById("categorydll").style.display = "block";
                  document.getElementById("categorytab").style.display = "none";


                  document.getElementById("canvasEditor").style.display = "none";
                  document.getElementById("can").style.display = "none";
                  document.getElementById("imglogo").style.display = "block";

                  document.getElementById('fileType').innerHTML = "PDF";

                  document.getElementById("canvastool").style.width = "300px";
                  document.getElementById("canvastool").style.marginLeft = "450px";
              }

          }

          document.getElementById("dragarea").style.width = "700px";
          document.getElementById("dragarea").style.height = "500px";

          $('#btnSubmitImg').click((e) => {
              var canvas = document.getElementById("can");
              var image = new Image();
              image.id = "pic";
              let imagetype = document.getElementById('fileType').textContent;
              let imagecntn = "";
              if (imagetype === 'JPG') {
                  image.src = canvas.toDataURL();
                  imagecntn = canvas.toDataURL('image/jpeg', 1);
              }
              else {
                  imagecntn = document.getElementById('pdfFile').textContent;
              }


              var ddlImgType = document.getElementById("MainContent_DocumentUpload_ModalDrawImageType");
              var ImgTypeValue = ddlImgType.value;
              var notesvalue = document.getElementById("modal-draw-image-notes").value;

              if (ImgTypeValue === "") {
                  toastr.error("You must select document type", 'ERROR', { timeOut: 5000 })
              }
              else {
                  if (item_imageO.length > 0) {
                      let soapdoc = soap_document.findIndex(f => f.image_id == item_imageO[0].image_id);
                      soap_document[soapdoc].image_url = canvas.toDataURL('image/jpeg', 1);
                      soap_document[soapdoc].image_type_value = ImgTypeValue;
                      soap_document[soapdoc].image_remark = notesvalue;
                  }
                  else {
                      soap_document.push({
                          image_id: uuidv4(),
                          patient_id: new URLSearchParams(window.location.search).get('idPatient'),
                          admission_id: new URLSearchParams(window.location.search).get('AdmissionId'),
                          image_url: imagecntn,
                          image_type_value: ImgTypeValue,
                          image_remark: "",
                          is_new: true,
                          is_delete: false,
                          image_format: imagetype
                      });
                  }

                  $('#modal-draw-image').modal('hide')

                  refreshImageObjective();
  
                  e.stopImmediatePropagation()
                  console.log(soap_document);
                  $('#jsonDataHolder').val(JSON.stringify(soap_document));
              }
          })

          $('#modal-draw-image').modal('show');
      }


      var mousePressed = false;
      var lastX, lastY;

      $('#can').mousedown(function (e) {
          mousePressed = true;
          Draw(e.pageX - $(this).offset().left, e.pageY - $(this).offset().top, false);
      });

      $('#can').mousemove(function (e) {
          if (mousePressed) {
              Draw(e.pageX - $(this).offset().left, e.pageY - $(this).offset().top, true);
          }
      });

      $('#can').mouseup(function (e) {
          if (mousePressed) {
              mousePressed = false;

              let textfill = $("#modal-draw-image-textadd").val();
              if (textfill == "" || textfill == null || textfill == undefined) {
                  cPush();
              }
          }
      });
      $('#can').mouseleave(function (e) {
          if (mousePressed) {
              mousePressed = false;
              cPush();
          }
      });

      function Draw(x, y, isDown) {
          if (isDown) {
              ctx.beginPath();
              ctx.strokeStyle = $('#selColor').val();
              ctx.lineWidth = $('#selWidth').val();
              ctx.lineJoin = "round";
              ctx.moveTo(lastX, lastY);
              ctx.lineTo(x, y);
              ctx.closePath();
              ctx.stroke();
          }
          lastX = x; lastY = y;
      }

      function clearArea() {
          // Use the identity matrix while clearing the canvas
          var canvasPic = new Image();
          canvasPic.src = cPushArray[0];
          canvasPic.onload = function () { ctx.drawImage(canvasPic, 0, 0); }
          cPush();
      }


      var canvas = document.getElementById('can');
      var ctx = canvas.getContext('2d');

      function btnUploadComputer() {
          document.getElementById("btnUploadSelect").style.display = "none";
          document.getElementById("uploadDiv").style.display = "flex";
          document.getElementById("dragarea").style.display = "flex";

          document.getElementById("categorytab").style.display = "none";
          //document.getElementById("categorydll").style.display = "flex";
          //document.getElementById("fugeneral").style.display = "none";
      }

      $("#coronary-tab").click(function (e) {
          useTemplate("coronary")
      });

      $("#lvgraphy-tab").click(function (e) {
          useTemplate("lvgrapgy")
      });

      function useTemplate(tmpname) {
          cPushArray = new Array();
          cStep = -1;
          var img = new Image();
          img.onload = function () {
              let imageDx = img.width + img.height;
              let imgdif = imageDx / 1200;

              canvas.width = img.width / imgdif;
              canvas.height = img.height / imgdif;
              ctx.drawImage(img, 0, 0, img.width, img.height,     // source rectangle
                  0, 0, canvas.width, canvas.height);
              cPush();
          }
          if (tmpname == "coronary") {
              img.src = window.location.origin + "/Images/Template/CoronaryAngiography.png";
          }
          else if (tmpname == "lvgrapgy") {
              img.src = window.location.origin + "/Images/Template/LVGraphy.png";
          }

          document.getElementById("btnUploadSelect").style.display = "none";
          document.getElementById("uploadDiv").style.display = "flex";
          document.getElementById("canvastool").style.display = "block";
          document.getElementById("canvasEditor").style.display = "block";
          document.getElementById("dragarea").style.display = "none";

          let elementddl = document.getElementById("modal-draw-image-type");
          if (tmpname == "coronary") {
              elementddl.value = "Coronary Angiography";
          }
          else if (tmpname == "lvgrapgy") {
              elementddl.value = "LV Graphy";
          }

          document.getElementById("categorytab").style.display = "flex";
          document.getElementById("categorydll").style.display = "none";
          document.getElementById('fileType').innerHTML = "JPG";

      }

      //selecting all required elements
      const dropArea = document.querySelector(".drag-area"),
          dragText = dropArea.querySelector("header"),
          button = dropArea.querySelector("button"),
          input = dropArea.querySelector("input");
      let file; //this is a global variable and we'll use it inside multiple functions

      input.addEventListener("change", function () {
          console.log(this.files);
          //getting user select file and [0] this means if user select multiple files then we'll select only the first one
          file = this.files[0];
          dropArea.classList.add("active");

          showFile(); //calling function
      });


      //If user Drag File Over DropArea
      dropArea.addEventListener("dragover", (event) => {
          event.preventDefault(); //preventing from default behaviour
          dropArea.classList.add("active");
          dragText.textContent = "Release to Upload File";
      });

      //If user leave dragged File from DropArea
      dropArea.addEventListener("dragleave", () => {
          dropArea.classList.remove("active");
          dragText.textContent = "Drag & Drop to Upload File";
      });

      //If user drop File on DropArea
      dropArea.addEventListener("drop", (event) => {
          event.preventDefault(); //preventing from default behaviour
          //getting user select file and [0] this means if user select multiple files then we'll select only the first one
          file = event.dataTransfer.files[0];
          showFile(); //calling function
      });

      function showFile() {
          cPushArray = new Array();
          cStep = -1;
          let fileType = file.type; //getting selected file type
          let validExtensions = ["image/jpeg", "image/jpg", "image/png"]; //adding some valid image extensions in array
          if (validExtensions.includes(fileType)) { //if user selected file is an image file
              let fileReader = new FileReader(); //creating new FileReader object
              fileReader.onload = (event) => {
                  let fileURL = fileReader.result; //passing user file source in fileURL variable
                  //let imgTag = `<img src="${fileURL}" alt="">`; //creating an img tag and passing user selected file source inside src attribute
                  var img = new Image();
                  img.onload = function () {
                      let imageDx = img.width + img.height;
                      let imgdif = imageDx / 1200;

                      canvas.width = img.width / imgdif;
                      canvas.height = img.height / imgdif;
                      ctx.drawImage(img, 0, 0, img.width, img.height,     // source rectangle
                          0, 0, canvas.width, canvas.height);
                      cPush();
                  }
                  img.src = event.target.result;
                  document.getElementById("canvastool").style.display = "block";
                  document.getElementById("canvasEditor").style.display = "block";
                  document.getElementById("categorydll").style.display = "block";
                  document.getElementById("can").style.display = "block";
                  document.getElementById("dragarea").style.display = "none";
                  document.getElementById("imglogo").style.display = "none";

                  //dropArea.innerHTML = imgTag; //adding that created img tag inside dropArea container
              }
              fileReader.readAsDataURL(file);

              document.getElementById("dragarea").style.removeProperty('width');
              document.getElementById("dragarea").style.removeProperty('height');

              document.getElementById('pdfFile').innerHTML = "";
              document.getElementById('fileType').innerHTML = "JPG";


          } else {
              if (file.name.split('.')[1] == "pdf" || file.name.split('.')[1] == "PDF") {
                  // FileReader function for read the file.
                  var fileReader = new FileReader();
                  var base64;
                  // Onload of file read the file content
                  fileReader.onload = function (fileLoadedEvent) {
                      base64 = fileLoadedEvent.target.result;
                      // Print data in console
                      console.log(base64);
                      document.getElementById('pdfFile').innerHTML = base64;
                  };
                  // Convert data to base64
                  fileReader.readAsDataURL(file);

                  document.getElementById("canvastool").style.display = "block";
                  document.getElementById("canvasEditor").style.display = "none";
                  document.getElementById("categorydll").style.display = "block";
                  document.getElementById("can").style.display = "none";
                  document.getElementById("dragarea").style.display = "none";
                  document.getElementById("imglogo").style.display = "block";


                  document.getElementById('fileType').innerHTML = "PDF";
              }
              else {
                  alert("This is not an Image File!");
                  dropArea.classList.remove("active");
                  dragText.textContent = "Drag & Drop to Upload File";
              }
          }
      }

      var cPushArray = new Array();
      var cStep = -1;

      function cPush() {
          cStep++;
          if (cStep < cPushArray.length) { cPushArray.length = cStep; }
          cPushArray.push(document.getElementById('can').toDataURL());
      }
      function cUndo() {
          if (cStep > 0) {
              cStep--;
              var canvasPic = new Image();
              canvasPic.src = cPushArray[cStep];
              canvasPic.onload = function () { ctx.drawImage(canvasPic, 0, 0); }
          }
      }
      function cRedo() {
          if (cStep < cPushArray.length - 1) {
              cStep++;
              var canvasPic = new Image();
              canvasPic.src = cPushArray[cStep];
              canvasPic.onload = function () { ctx.drawImage(canvasPic, 0, 0); }
          }
      }

      $("#modal-draw-image").on("shown.bs.modal", function () {
          cPushArray = new Array();
          cStep = -1;
          cPush();
      });

      $("#can").click(function (e) {
          var x = e.pageX - $(this).offset().left;
          var y = e.pageY - $(this).offset().top;

          var addtext = $("#modal-draw-image-textadd").val();
          if (addtext) {
              ctx.font = "14px Arial";
              ctx.fillText(addtext, x, y);
              $("#modal-draw-image-textadd").val("");
              cPush();
          }
      });


      function refreshImageObjective() {

          $('.list-draw-image').empty()

          $(soap_document).each((idx, item) => {
              if (!item.is_delete) {
                  if (item.image_format == "JPG") {
                      $('.list-draw-image').append('<li>' +
                          '<div class="row" style="width: 100%; border: 1px solid silver;border-width: thin;height: 50px;padding: 10px;border-radius: 8px 8px 0 0;">' +
                          '<div class="col-sm-3"><b>' + item.image_type_value + '</b></div>' +
                          '<div class="col-sm-2 text-right">' +
                          '<a href="javascript:void(0)" title="Edit"' +
                          'class="edit-imgs btn-edit-grid"' +
                          ' data-img-id="' + item.image_id +
                          '"><i class="fa fa-pencil"></i>Edit</a>' +
                          '</div> ' +
                          '<div class="col-sm-1 text-right">' +
                          '<a href="javascript:void(0)" title="Delete"' +
                          'class="delete-imgs btn-delete-grid"' +
                          ' data-img-id="' + item.image_id +
                          '"><i class="fa fa-trash"></i>Delete</a>' +
                          '</div>' +
                          '</div>' +
                          '<div class="row" style="width: 100%; border: 1px solid silver;border-width: thin;padding: 10px;border-radius: 0 0 8px 8px;">' +
                          '<div class="col-sm-5"><div class="row">' +
                          '<img class="edit-imgs" style="max-width:500px;" data-img-id="' + item.image_id + '" src="' + item.image_url + '" alt=""></div>' +
                          '<label style="font-size: 12px;"><i>' + item.upload_date + '</i></label></div>' +
                          '<div class="col-sm-5"><div class="row">' +
                             '<label style="margin-left: 8px;"><b>Remarks</b></label></div><textarea class="edit-notes" data-img-id="' + item.image_id + '" style="max-width:500px; width:500px;" rows="10">' + item.image_remark + '</textarea></div>' +
                          '</div>' +
                          '</li>')

                  }
                  else {
                      $('.list-draw-image').append('<li>' +
                          '<div class="row" style="width: 100%; border: 1px solid silver;border-width: thin;height: 50px;padding: 10px;border-radius: 8px 8px 0 0;">' +
                          '<div class="col-sm-3"><b>' + item.image_type_value + '</b></div>' +
                          '<div class="col-sm-2 text-right">' +
                          '<a href="javascript:void(0)" title="Edit"' +
                          'class="edit-imgs btn-edit-grid"' +
                          ' data-img-id="' + item.image_id +
                          '"><i class="fa fa-pencil"></i>Edit</a>' +
                          '</div> ' +
                          '<div class="col-sm-1 text-right">' +
                          '<a href="javascript:void(0)" title="Delete"' +
                          'class="delete-imgs btn-delete-grid"' +
                          ' data-img-id="' + item.image_id +
                          '"><i class="fa fa-trash"></i>Delete</a>' +
                          '</div>' +
                          '</div>' +
                          '<div class="row" style="width: 100%; border: 1px solid silver;border-width: thin;padding: 10px;border-radius: 0 0 8px 8px;">' +
                          '<div class="col-sm-5"><div class="row">' +
                          '<img class="view-pdf" data-img-id="' + item.image_id + '" src=' + "../../../../Images/Icon/pdficon.png" + ' alt=""></div>' +
                          '<label style="font-size: 12px;"><i>' + item.upload_date + '</i></label></div>' +
                          '<div class="col-sm-5"><div class="row">' +
                             '<label style="margin-left: 8px;"><b>Remarks</b></label></div><textarea class="edit-notes" data-img-id="' + item.image_id + '" style="max-width:500px; width:500px;" rows="10">' + item.image_remark + '</textarea></div>' +
                          '</div>' +
                          '</li>')
                  }
              }
          })



          //DELETE LABS
          $('.delete-imgs').on('click', (e) => {
              e.preventDefault()

              let img_id = e.currentTarget.getAttribute('data-img-id')

              let idx = soap_document.findIndex(f => f.image_id === img_id)

              if (idx !== -1) {
                  if (soap_document[idx].is_new) {
                      soap_document.splice(idx, 1)
                  }
                  else {
                      soap_document[idx].is_delete = true;
                  }

                  toastr.success('Delete image', 'Success', { timeOut: 4000 })

                  $('#jsonDataHolder').val(JSON.stringify(soap_document));

                  refreshImageObjective()
              }
          })

          $('.edit-imgs').on('click', (e) => {
              item_imageO = [];
              let id = e.currentTarget.getAttribute('data-img-id')
              let idxh = soap_document.findIndex(f => f.image_id === id)

              item_imageO.push(soap_document[idxh]);


              OpenDrawImage();
              e.stopImmediatePropagation()

              refreshImageObjective()
          })

          $('.view-pdf').on('click', (e) => {
              item_imageO = [];
              let id = e.currentTarget.getAttribute('data-img-id')
              let idxh = soap_document.findIndex(f => f.image_id === id)

              const pdfbase64 = soap_document[idxh].image_url.replace("data:application/pdf;base64", "");
              const blob = b64toBlob(pdfbase64);
              var url = URL.createObjectURL(blob);

              window.open(url)

              e.stopImmediatePropagation()
          })

          function b64toBlob(dataURI) {

              var byteString = atob(dataURI.split(',')[1]);
              var ab = new ArrayBuffer(byteString.length);
              var ia = new Uint8Array(ab);

              for (var i = 0; i < byteString.length; i++) {
                  ia[i] = byteString.charCodeAt(i);
              }
              return new Blob([ab], { type: 'application/pdf' });
          }

          $('.edit-notes').on('change', (e) => {
              item_imageO = [];
              let id = e.currentTarget.getAttribute('data-img-id')
              let idx = soap_document.findIndex(f => f.image_id === id)
              if (idx > -1)
                  soap_document[idx].image_remark = e.currentTarget.value

              $('#jsonDataHolder').val(JSON.stringify(soap_document));

              e.stopImmediatePropagation()
          })

      }

      function uuidv4() {
          return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
              var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
              return v.toString(16);
          });
      }

  </script>
  <style>

modal-body{
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
}
.drag-area{
  border: 2px dashed #5256ad;
  border-radius: 5px;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}
.drag-area.active{
  border: 2px solid #5256ad;
}
.drag-area .icon{
  font-size: 100px;
  color: #5256ad;
}
.drag-area header{
  font-size: 30px;
  font-weight: 500;
  color: #5256ad;
}
.drag-area span{
  font-size: 25px;
  font-weight: 500;
  color: #5256ad;
  margin: 10px 0 15px 0;
}
.drag-area button{
  padding: 10px 25px;
  font-size: 20px;
  font-weight: 500;
  border: none;
  outline: none;
  background: #5256ad;
  color: #fff;
  border-radius: 5px;
  cursor: pointer;
}
.drag-area img{
  height: 100%;
  object-fit: cover;
  border-radius: 5px;
}
#canvastool{
	display: none;
}
#can{
  height: 100%;
  object-fit: cover;
  border-radius: 5px;
}
.btn-delete-grid {
    color:red;
}
  </style>

