using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopBridges.Business;
using ShopBridges.Business.Model;

namespace ShopBridges.Controllers
{

    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductBL productBL;
        private readonly ILogger logger;

        public ProductController(ProductBL _proudctBL, ILogger<ProductController> _logger)
        {
            productBL = _proudctBL;
            logger = _logger;
        }

        [HttpGet]
        [Route("api/Product/GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = null;
            try
            {
                products = await productBL.GetProducts();
            }
            catch (Exception ex)
            {
                //Log exception here
                logger.LogError(ex.ToString());
            }

            return Ok(products);
        }

        [HttpPost]
        [Route("api/Product/SaveProduct")]
        public async Task<IActionResult> SaveProduct([FromBody] Product product)
        {
            bool result = false;
            IActionResult httpResponseMessage = null;

            try
            {
                if (ModelState.IsValid)
                {
                    result = await productBL.SaveProduct(product);

                    if (result)
                    {
                        httpResponseMessage = new ContentResult()
                        {
                            StatusCode = (int)HttpStatusCode.Created
                        };
                    }
                    else
                    {

                        httpResponseMessage = new ContentResult()
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Content = "Unable to save the product"
                        };
                    }
                }
                else
                {
                    httpResponseMessage = new ContentResult()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Content = "Input model is not correct."
                    };
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                logger.LogError(ex.ToString());

                httpResponseMessage = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Content = "Error Occurred " + ex.Message
                };
            }
            return httpResponseMessage;
        }

        [HttpPost]
        [Route("api/Product/ModifyProduct")]
        public async Task<IActionResult> ModifyProduct([FromBody] Product product)
        {
            bool result = false;
            IActionResult httpResponseMessage = null;
            try
            {
                if (ModelState.IsValid)
                {
                    result = await productBL.ModifyProuct(product);

                    if (result)
                    {
                        httpResponseMessage = new ContentResult()
                        {
                            StatusCode = (int)HttpStatusCode.OK
                        };
                    }
                    else
                    {

                        httpResponseMessage = new ContentResult()
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Content = "Unable to modify the product"
                        };
                    }
                }
                else
                {
                    httpResponseMessage = new ContentResult()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Content = "Input model is not correct."
                    };
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                logger.LogError(ex.ToString());

                httpResponseMessage = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Content = "Error Occurred:: " + ex.Message
                };
            }

            return httpResponseMessage;
        }


        [HttpGet]
        [Route("api/Product/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
        {
            bool result = false;
            IActionResult httpResponseMessage = null;
            try
            {

                result = await productBL.DeleteProduct(productId);

                if (result)
                {
                    httpResponseMessage = new ContentResult()
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {

                    httpResponseMessage = new ContentResult()
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Content = "Unable to delete the product"
                    };
                }

            }
            catch (Exception ex)
            {
                //Log exception here
                logger.LogError(ex.ToString());

                httpResponseMessage = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Content = "Error Occurred:: " + ex.Message
                };
            }

            return httpResponseMessage;
        }
    }
}