using BlueOfficeClient.Entities;
using ClientInfrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BlueOffice.Service
{
	public class FileUploadService
	{
		private readonly ILog Logger = LogManager.GetLog(typeof(FileUploadService));

		private string _url;
		private string _accessToken;
		private string _contentType;

		public FileUploadService(string url, string accessToken)
		{
			_url = url;
			_accessToken = accessToken;
		}


		public void UploadFile(UploadFileInfo fileInfo, Action<HttpStatusCode, string> callback)
		{
			UploadFile(fileInfo.Stream, fileInfo.FileName, null, callback);
		}

		public void UploadFile(Stream fileDataStream, string uploadedFileName, string fileFormName, Action<HttpStatusCode, string> callback)
		{
			if (string.IsNullOrEmpty(fileFormName))
			{
				fileFormName = "attachmentFile";
			}
			var multipartFormStream = CreateMultipartFormData(fileDataStream, uploadedFileName, fileFormName);
			PostData(multipartFormStream, callback);
		}

		private MemoryStream CreateMultipartFormData(Stream fileDataStream, string uploadedFileName, string fileFormName)
		{
			var multipartFormStream = new MemoryStream();

			const string end = "\r\n";
			const string twoHyphens = "--";
			var boundary = string.Format("--------------WI{0}", Guid.NewGuid().ToString("N"));
			var contentType = string.Format("multipart/form-data; boundary={0}", boundary);

			_contentType = contentType;

			var contentDisposition = "Content-Disposition: form-data; name=\"" + fileFormName + "\"; filename=\"" + uploadedFileName + "\"";

			var twoHyphensbytes = Encoding.UTF8.GetBytes(twoHyphens);
			var boundaryBytes = Encoding.UTF8.GetBytes(boundary);
			var endBytes = Encoding.UTF8.GetBytes(end);
			var contentDispositionbytes = Encoding.UTF8.GetBytes(contentDisposition);
			var contentTypebytes = Encoding.UTF8.GetBytes("Content-Type: application/octet-stream");

			var boundaryStartStream = new MemoryStream();
			boundaryStartStream.Write(endBytes, 0, endBytes.Length);
			boundaryStartStream.Write(twoHyphensbytes, 0, twoHyphensbytes.Length);
			boundaryStartStream.Write(boundaryBytes, 0, boundaryBytes.Length);
			boundaryStartStream.Write(endBytes, 0, endBytes.Length);
			boundaryStartStream.Write(contentDispositionbytes, 0, contentDispositionbytes.Length);
			boundaryStartStream.Write(endBytes, 0, endBytes.Length);
			boundaryStartStream.Write(contentTypebytes, 0, contentTypebytes.Length);
			boundaryStartStream.Write(endBytes, 0, endBytes.Length);
			boundaryStartStream.Write(endBytes, 0, endBytes.Length);
			boundaryStartStream.Position = 0;

			//fileDataStream.CopyTo(memoryStream);

			var boundaryEndStream = new MemoryStream();
			boundaryEndStream.Write(endBytes, 0, endBytes.Length);
			boundaryEndStream.Write(twoHyphensbytes, 0, twoHyphensbytes.Length);
			boundaryEndStream.Write(boundaryBytes, 0, boundaryBytes.Length);
			boundaryEndStream.Write(twoHyphensbytes, 0, twoHyphensbytes.Length);
			boundaryEndStream.Write(endBytes, 0, endBytes.Length);
			boundaryEndStream.Position = 0;

			fileDataStream.Position = 0;
			boundaryStartStream.CopyTo(multipartFormStream);
			fileDataStream.CopyTo(multipartFormStream);
			boundaryEndStream.CopyTo(multipartFormStream);

			multipartFormStream.Position = 0;

			return multipartFormStream;
		}

		private void PostData(Stream outputStream, Action<HttpStatusCode, string> callback)
		{
			var request = (HttpWebRequest)WebRequest.Create(_url);
			request.ContentType = _contentType;
			request.Method = "POST";
			if (!string.IsNullOrEmpty(_accessToken))
			{
				request.Headers["AccessToken"] = _accessToken;
			}


			request.BeginGetRequestStream((reqResult) =>
			{
				try
				{
					var _request = (HttpWebRequest)reqResult.AsyncState;
					using (var stream = _request.EndGetRequestStream(reqResult))
					{
						outputStream.CopyTo(stream);
					}
					_request.BeginGetResponse((resResult) =>
					{
						try
						{
							var _currentRequest = (HttpWebRequest)reqResult.AsyncState;
							var response = (HttpWebResponse)_currentRequest.EndGetResponse(resResult);
							var result = string.Empty;
							using (var stream = response.GetResponseStream())
							{
								var reader = new StreamReader(stream);
								result = reader.ReadToEnd();
							}

							callback(response.StatusCode, result);

						}
						catch (WebException wexp)
						{
							Logger.ErrorException("Upload File Error:", wexp);
							callback(
								wexp.Response == null
									? HttpStatusCode.InternalServerError
									: ((HttpWebResponse)wexp.Response).StatusCode, string.Empty);

						}
						catch (Exception exp)
						{
							Logger.ErrorException("Upload File Error:", exp);
							callback(HttpStatusCode.InternalServerError, string.Empty);

						}
					}, _request);
				}

				catch (WebException wexp)
				{
					Logger.ErrorException("Upload File Error:", wexp);
					callback(
						wexp.Response == null
							? HttpStatusCode.InternalServerError
							: ((HttpWebResponse)wexp.Response).StatusCode, string.Empty);

				}
				catch (Exception exp)
				{
					Logger.ErrorException("Upload File Error:", exp);
					callback(HttpStatusCode.InternalServerError, string.Empty);

				}
			}, request);

		}

	}

}