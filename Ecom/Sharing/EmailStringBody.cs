namespace Ecom.Core.Sharing
{
    public class EmailStringBody
    {
        public static string Send(string email, string otp, string message)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
  <style>
    body {{
      font-family: 'Arial', sans-serif;
      background-color: #f4f4f4;
      margin: 0;
      padding: 0;
    }}
    .container {{
      max-width: 480px;
      margin: 40px auto;
      background: #ffffff;
      border-radius: 12px;
      box-shadow: 0 4px 20px rgba(0,0,0,0.1);
      overflow: hidden;
    }}
    .header {{
      background: linear-gradient(45deg, #ff7e5f, #feb47b);
      padding: 30px 20px;
      text-align: center;
      color: #fff;
      font-size: 22px;
      font-weight: bold;
    }}
    .body {{
      padding: 30px 20px;
      text-align: center;
      color: #333;
    }}
    .body p {{
      font-size: 15px;
      margin-bottom: 24px;
    }}
    .otp-box {{
      display: inline-block;
      letter-spacing: 10px;
      font-size: 36px;
      font-weight: bold;
      color: #ff7e5f;
      background: #fff5f0;
      border: 2px dashed #feb47b;
      border-radius: 10px;
      padding: 16px 32px;
      margin: 10px 0 24px;
    }}
    .expiry {{
      font-size: 13px;
      color: #999;
      margin-top: 10px;
    }}
    .footer {{
      background: #f9f9f9;
      padding: 16px;
      text-align: center;
      font-size: 12px;
      color: #aaa;
      border-top: 1px solid #eee;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">My Ecom</div>
    <div class=""body"">
      <p>{message}</p>
      <div class=""otp-box"">{otp}</div>
      <p class=""expiry"">This code expires in <strong>10 minutes</strong>.<br/>Do not share it with anyone.</p>
    </div>
    <div class=""footer"">
      If you did not request this, please ignore this email.<br/>
      &copy; {DateTime.UtcNow.Year} My Ecom
    </div>
  </div>
</body>
</html>";
        }
    }
}