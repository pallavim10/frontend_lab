<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CTMS.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html class="bg-black">
<head id="Head1" runat="server">
    <title></title>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

    <script type="text/javascript">

        $.getJSON("https://api.ipify.org/?format=json", function (e) {
            console.log(e.ip);
        });


        function refreshScripts() {

            (function () {
                var h, a, f;
                a = document.getElementsByTagName('link');
                for (h = 0; h < a.length; h++) {
                    f = a[h];
                    if (f.rel.toLowerCase().match(/stylesheet/) && f.href) {
                        var g = f.href.replace(/(&|\?)rnd=\d+/, '');
                        f.href = g + (g.match(/\?/) ? '&' : '?');
                        f.href += 'rnd=' + (new Date().valueOf());
                    }
                } // for
            })()

        }

    </script>
    <style>
        .form-control {
            display: block;
            padding: 1px 12px;
            font-size: 11px;
            width: 100%;
            height: 30px;
            line-height: 1.428571429;
            color: #555555;
            vertical-align: middle;
            background-color: #ffffff;
            background-image: none;
            border: 1px solid #cccccc;
            border-radius: 4px;
        }

        body {
            margin: 0;
            color: #6a6f8c;
            background: #c8c8c8;
            font: 600 16px/18px 'Open Sans',sans-serif;
        }

        *, :after, :before {
            box-sizing: border-box;
        }

        .clearfix:after, .clearfix:before {
            content: '';
            display: table;
        }

        .clearfix:after {
            clear: both;
            display: block;
        }

        a {
            color: inherit;
            text-decoration: none;
        }

        .login-wrap {
            margin-top: 8% !important;
            width: 100%;
            margin: auto;
            max-width: 525px;
            min-height: 400px;
            position: relative;
            background: url(Images/Test1.jpeg) no-repeat center;
            box-shadow: 0 12px 15px 0 rgba(0,0,0,.24),0 17px 50px 0 rgba(0,0,0,.19);
            border-radius: 27px;
        }

        .login-html {
            width: 100%;
            height: 100%;
            position: absolute;
            padding: 32px 70px 50px 70px;
            background: rgba(40,57,101,.9);
            border-radius: 27px;
        }

            .login-html .sign-in-htm, .login-html .sign-up-htm {
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                position: absolute;
                transform: rotateY(180deg);
                backface-visibility: hidden;
                transition: all .4s linear;
            }

            .login-html .sign-in, .login-html .sign-up, .login-form .group .check {
                display: none;
            }

            .login-html .tab, .login-form .group .label, .login-form .group .button {
                text-transform: uppercase;
            }

            .login-html .tab {
                font-size: 22px;
                margin-right: 15px;
                padding-bottom: 5px;
                margin: 0 15px 10px 0;
                display: inline-block;
                border-bottom: 2px solid transparent;
            }

            .login-html .sign-in:checked + .tab, .login-html .sign-up:checked + .tab {
                color: #fff;
                border-color: #1161ee;
            }

        .login-form {
            min-height: 345px;
            position: relative;
            perspective: 1000px;
            transform-style: preserve-3d;
        }

            .login-form .group {
                margin-bottom: 15px;
            }

                .login-form .group .label, .login-form .group .input, .login-form .group .button {
                    width: 100%;
                    color: #fff;
                    display: block;
                }

                .login-form .group .input, .login-form .group .button {
                    border: none;
                    padding: 15px 20px;
                    border-radius: 25px;
                    background: rgba(255,255,255,.1);
                }

                .login-form .group input[data-type="password"] {
                    text-security: circle;
                    -webkit-text-security: circle;
                }

                .login-form .group .label {
                    color: #aaa;
                    font-size: 12px;
                }

                .login-form .group .button {
                    background: #7591c3b3;
                }

                .login-form .group label .icon {
                    width: 15px;
                    height: 15px;
                    border-radius: 2px;
                    position: relative;
                    display: inline-block;
                    background: rgba(255,255,255,.1);
                }

                    .login-form .group label .icon:before, .login-form .group label .icon:after {
                        content: '';
                        width: 10px;
                        height: 2px;
                        background: #fff;
                        position: absolute;
                        transition: all .2s ease-in-out 0s;
                    }

                    .login-form .group label .icon:before {
                        left: 3px;
                        width: 5px;
                        bottom: 6px;
                        transform: scale(0) rotate(0);
                    }

                    .login-form .group label .icon:after {
                        top: 6px;
                        right: 0;
                        transform: scale(0) rotate(0);
                    }

                .login-form .group .check:checked + label {
                    color: #fff;
                }

                    .login-form .group .check:checked + label .icon {
                        background: #1161ee;
                    }

                        .login-form .group .check:checked + label .icon:before {
                            transform: scale(1) rotate(45deg);
                        }

                        .login-form .group .check:checked + label .icon:after {
                            transform: scale(1) rotate(-45deg);
                        }

        .login-html .sign-in:checked + .tab + .sign-up + .tab + .login-form .sign-in-htm {
            transform: rotate(0);
        }

        .login-html .sign-up:checked + .tab + .login-form .sign-up-htm {
            transform: rotate(0);
        }

        .hr {
            height: 2px;
            margin: 60px 0 50px 0;
            background: rgba(255,255,255,.2);
        }

        .foot-lnk {
            text-align: center;
        }

        .forgot-password {
            text-align: right;
            margin-top: 10px;
            font-size: small;
            margin-bottom: 10px;
        }

            .forgot-password a {
                text-decoration: none;
                color: #fff;
            }

        ::placeholder {
            color: white;
            opacity: 1; /* Firefox */
        }
    </style>
    <style>
        .button-container {
            text-align: right; /* Aligns its child elements to the right */
        }
    </style>
</head>
<body>
    <script>
        $(document).ready(function () {
            $.ajax({
                url: 'https://api.myip.com',
                method: 'GET',
                dataType: 'json',
                success: function (data) {
                    console.log(data);
                },
                error: function (error) {
                    console.error('Error fetching JSON:', error);
                }
            });
        });


        // Call this function during login
        function handleLogin() {
            document.cookie = "sessionTimeout=" + new Date() + "; path=/"; // Reset the sessionTimeout cookie
        }
    </script>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hdn" Value="0" />
        <asp:HiddenField runat="server" ID="hdnIP" Value="ABCD" />
        <asp:HiddenField runat="server" ID="hdnCOUNTRY" Value="ABCD" />
        <div class="login-wrap">
            <div class="login-html">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-size: 17px; font-weight: bold;"
                    ForeColor="Red"></asp:Label>
                <br />
                <br />
                <input id="tab-1" type="radio" name="tab" class="sign-in" checked><label for="tab-1"
                    class="tab">LOGIN</label>
                <input id="tab-2" type="radio" name="tab" class="sign-up"><label for="tab-2" class="tab"></label>
                <div class="login-form">
                    <div id="divLogin" runat="server">
                        <div class="sign-in-htm">
                            <br />
                            <div class="group">
                                <asp:TextBox ID="txt_UserName" runat="server" type="text" required="true" placeholder="USER ID" class="input"></asp:TextBox>
                            </div>
                            <br />
                            <div class="group">
                                <asp:TextBox ID="txt_Pwd" runat="server" TextMode="Password" type="password" placeholder="PASSWORD" class="input"
                                    required="true" data-type="password"></asp:TextBox>
                            </div>
                            <br />
                            <div class="group">
                                <asp:Button ID="Btn_Login" runat="server" type="submit" class="button" value="LOGIN" OnClientClick="handleLogin();" OnClick="Btn_Login_Click"
                                    Text="Login" />
                            </div>
                            <div class="forgot-password">
                                <a href="Forgot_Password.aspx">Forgot password?</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
