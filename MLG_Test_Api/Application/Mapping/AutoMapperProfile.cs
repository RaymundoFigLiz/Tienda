using AutoMapper;
using MLG_Test.Application.DTOs;
using MLG_Test.Core.Models;

namespace MLG_Test.Application.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			// Client
			CreateMap<RegisterRequestDTO, Client>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
			CreateMap<RegisterRequestDTO, Address>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.AddressName))
				.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
				.ForMember(dest => dest.ExternalNumber, opt => opt.MapFrom(src => src.ExternalNumber))
				.ForMember(dest => dest.InternalNumber, opt => opt.MapFrom(src => src.InternalNumber));
			CreateMap<RegisterRequestDTO, User>()
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.FirstLastName, opt => opt.MapFrom(src => src.FirstLastName))
				.ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.SecondLastName))
				.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));

			// Item
			CreateMap<CreateItemDTO, Item>();
			CreateMap<UpdateItemDTO, Item>();

			// Store
			CreateMap<Store, StoreDTO>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Description))
				.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
				.ForMember(dest => dest.ExternalNumber, opt => opt.MapFrom(src => src.Address.ExternalNumber))
				.ForMember(dest => dest.InternalNumber, opt => opt.MapFrom(src => src.Address.InternalNumber));
			CreateMap<CreateStoreDTO, Store>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));
			CreateMap<CreateStoreDTO, Address>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Address))
				.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
				.ForMember(dest => dest.ExternalNumber, opt => opt.MapFrom(src => src.ExternalNumber))
				.ForMember(dest => dest.InternalNumber, opt => opt.MapFrom(src => src.InternalNumber));
			CreateMap<UpdateStoreDTO, Store>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));
			CreateMap<UpdateStoreDTO, Address>()
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Address))
				.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
				.ForMember(dest => dest.ExternalNumber, opt => opt.MapFrom(src => src.ExternalNumber))
				.ForMember(dest => dest.InternalNumber, opt => opt.MapFrom(src => src.InternalNumber));

			// Store Item
			CreateMap<StoreItem, StoreItemDTO>()
				.ForMember(dest => dest.ItemCode, opt => opt.MapFrom(src => src.Item.Code))
				.ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Description))
				.ForMember(dest => dest.ItemImage, opt => opt.MapFrom(src => src.Item.Image))
				.ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name));
			CreateMap<CreateStoreItemDTO, StoreItem>();
			CreateMap<UpdateStoreItemDTO, StoreItem>();

			// Sale
			CreateMap<Sale, SaleDTO>()
				.ForMember(dest => dest.ClientFullName, opt => opt.MapFrom(src =>
					src.Client.User.Name
					+ " "
					+ src.Client.User.FirstLastName
					+ (string.IsNullOrEmpty(src.Client.User.SecondLastName) ? (" " + src.Client.User.SecondLastName) : "")
				))
				.ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.StoreItem.Item.Description))
				.ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.StoreItem.Store.Name));
			CreateMap<CreateSaleDTO, Sale>();
			CreateMap<UpdateSaleDTO, Sale>();

		}
	}
}
