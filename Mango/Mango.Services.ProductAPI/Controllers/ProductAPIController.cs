﻿using AutoMapper;
using Azure;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> productList = _db.Products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(productList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;  
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product product = _db.Products.First(t => t.ProductId == id);
                _responseDto.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post(ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _db.Products.Add(product);
                _db.SaveChanges();
                _responseDto.Result = product;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put(ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _db.Products.Update(product);
                _db.SaveChanges();
                _responseDto.Result = product;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product product = _db.Products.First(t => t.ProductId == id);
                _db.Products.Remove(product);
                _db.SaveChanges();
                _responseDto.Result = product;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
